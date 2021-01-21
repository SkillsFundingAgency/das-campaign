using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Shared.Basket.Interfaces;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class GetBasketHandler : IRequestHandler<GetBasketQuery, ApprenticeshipFavouritesBasketRead>
    {
        private readonly ILogger<GetBasketHandler> _logger;
        private readonly IApprenticeshipFavouritesBasketStore _basketStore;
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;
        private readonly IGetProviderDetails _getProvider;

         public GetBasketHandler(
            ILogger<GetBasketHandler> logger,
            IApprenticeshipFavouritesBasketStore basketStore, 
            IGetStandards getStandards,
            IGetFrameworks getFrameworks,
            IGetProviderDetails getProvider)
        {
            _logger = logger;
            _basketStore = basketStore;
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _getProvider = getProvider;
        }

        public async Task<ApprenticeshipFavouritesBasketRead> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting basket for {basketId}", request.BasketId);

            var basket = await _basketStore.GetAsync(request.BasketId);

            ApprenticeshipFavouritesBasketRead basketRead = null;

            if (basket != null && basket.Any())
            {
                basketRead = new ApprenticeshipFavouritesBasketRead(basket);

                Parallel.ForEach(basketRead, (basketItem) =>
                {
                  EnrichApprenticeshipInfo(basketItem);
                });
            }

            return basketRead ?? new ApprenticeshipFavouritesBasketRead();
        }

        private void EnrichApprenticeshipInfo(ApprenticeshipFavouriteRead apprenticeship)
        {
            var apprenticeshipRead = new ApprenticeshipFavouriteRead(){ApprenticeshipId = apprenticeship.ApprenticeshipId};
            if (IsFramework(apprenticeship.ApprenticeshipId))
            {
                EnrichFramework(apprenticeship);
            }
            else
            {
                EnrichStandard(apprenticeship);
            }

            EnrichTrainingProvider(apprenticeship);

        }

        private void EnrichTrainingProvider(ApprenticeshipFavouriteRead apprenticeship)
        {
            foreach (var providerItem in apprenticeship.Providers)
            {
                var providerResult = _getProvider.GetProviderDetails(providerItem.Ukprn).GetAwaiter().GetResult();

                if (providerResult != null)
                {
                    providerItem.Name = providerResult.ProviderName;
                    providerItem.Active = true;
                }
            }

        }

        private void EnrichFramework(ApprenticeshipFavouriteRead apprenticeship)
        {
            var framework = _getFrameworks.GetFrameworkById(apprenticeship.ApprenticeshipId);

            apprenticeship.Title = framework.Title;
            apprenticeship.Duration = framework.Duration;
            apprenticeship.Level = framework.Level;
            apprenticeship.EffectiveTo = framework.EffectiveTo;
            apprenticeship.ApprenticeshipType = ApprenticeshipType.Framework;
            apprenticeship.Active = framework.IsActiveFramework;
        }

        private void EnrichStandard(ApprenticeshipFavouriteRead apprenticeship)
        {
            var standard = _getStandards.GetStandardById(apprenticeship.ApprenticeshipId);

            apprenticeship.Title = standard.Title;
            apprenticeship.Duration = standard.Duration;
            apprenticeship.Level = standard.Level;
            apprenticeship.EffectiveTo = standard.EffectiveTo;
            apprenticeship.ApprenticeshipType = ApprenticeshipType.Standard;
            apprenticeship.Active = standard.IsActiveStandard;
        }

        public bool IsFramework(string id)
        {
            return id.Contains("-");
        }
    }
}
