using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.Providers.Api.Client;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Basket.Interfaces;
using Sfa.Das.Sas.Shared.Basket.Models;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class AddorRemoveFavouriteInBasketCommandHandler : IRequestHandler<AddOrRemoveFavouriteInBasketCommand, AddOrRemoveFavouriteInBasketResponse>
    {
        private readonly IApprenticeshipFavouritesBasketStore _basketStore;
        private IProviderApiClient _providerApiClient;
        private readonly IMediator _mediator;
        private readonly ILogger<AddorRemoveFavouriteInBasketCommandHandler> _logger;

        public AddorRemoveFavouriteInBasketCommandHandler(ILogger<AddorRemoveFavouriteInBasketCommandHandler> logger, IApprenticeshipFavouritesBasketStore basketStore, IProviderApiClient providerApiClient, IMediator mediator)
        {
            _basketStore = basketStore;
            _providerApiClient = providerApiClient;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<AddOrRemoveFavouriteInBasketResponse> Handle(AddOrRemoveFavouriteInBasketCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ApprenticeshipId))
            {
                var message = $"Apprenticeship id must be provided for {nameof(AddorRemoveFavouriteInBasketCommandHandler)}";
                _logger.LogWarning(message);
                throw new ArgumentException(message);
            }

            string apprenticeshipName;
            if (request.ApprenticeshipId.Contains("-"))
            {
                var framework = await _mediator.Send(new GetFrameworkQuery { Id = request.ApprenticeshipId }, cancellationToken);
                apprenticeshipName = framework.Framework.Title;
            }
            else
            {
                var standard = await _mediator.Send(new GetStandardQuery { Id = request.ApprenticeshipId }, cancellationToken);
                apprenticeshipName = standard.Standard.Title;
            }

            var basketOperation = BasketOperation.Added;

            bool basketChanged;
            var basket = await GetBasket(request);

            if (basket == null)
            {
                basketChanged = true;
                basket = CreateNewBasket(request);
            }
            else
            {
                if (request.Ukprn.HasValue && request.LocationId.HasValue)
                {
                    if (basket.IsInBasket(request.ApprenticeshipId, request.Ukprn.Value, request.LocationId.Value))
                    {
                        basket.Remove(request.ApprenticeshipId, request.Ukprn.Value, request.LocationId.Value);
                        basketChanged = true;
                        basketOperation = BasketOperation.Removed;
                    }
                    else
                    {
                        var providerName = _providerApiClient.Get(request.Ukprn.Value).ProviderName;

                        basketChanged = basket.Add(request.ApprenticeshipId, request.Ukprn.Value, providerName, request.LocationId.Value);
                    }
                }
                else if (request.Ukprn.HasValue)
                {
                    if (basket.IsInBasket(request.ApprenticeshipId, request.Ukprn.Value))
                    {
                        basket.Remove(request.ApprenticeshipId, request.Ukprn.Value);
                        basketChanged = true;
                        basketOperation = BasketOperation.Removed;
                    }
                    else
                    {
                        var providerName = _providerApiClient.Get(request.Ukprn.Value).ProviderName;
                        basketChanged = basket.Add(request.ApprenticeshipId, request.Ukprn.Value, providerName);
                    }
                }
                else
                {
                    if (basket.IsInBasket(request.ApprenticeshipId))
                    {
                        basket.Remove(request.ApprenticeshipId);
                        basketChanged = true;
                        basketOperation = BasketOperation.Removed;
                    }
                    else
                    {
                        basketChanged = basket.Add(request.ApprenticeshipId);
                    }
                }
            }

            if (basketChanged)
            {
                await _basketStore.UpdateAsync(basket);
            }

            _logger.LogDebug("Updated apprenticeship basket: {basketId}", basket.Id);

            return new AddOrRemoveFavouriteInBasketResponse { BasketId = basket.Id, ApprenticeshipName = apprenticeshipName, BasketOperation = basketOperation };
        }

        private ApprenticeshipFavouritesBasket CreateNewBasket(AddOrRemoveFavouriteInBasketCommand request)
        {
            var basket = new ApprenticeshipFavouritesBasket();

            if (request.Ukprn.HasValue && request.LocationId.HasValue)
            {
                var providerName = _providerApiClient.Get(request.Ukprn.Value).ProviderName;
                basket.Add(request.ApprenticeshipId, request.Ukprn.Value, providerName, request.LocationId.Value);
            }
            else if (request.Ukprn.HasValue)
            {
                var providerName = _providerApiClient.Get(request.Ukprn.Value).ProviderName;
                basket.Add(request.ApprenticeshipId, request.Ukprn.Value, providerName);
            }
            else
            {
                basket.Add(request.ApprenticeshipId);
            }

            return basket;
        }

        private async Task<ApprenticeshipFavouritesBasket> GetBasket(AddOrRemoveFavouriteInBasketCommand request)
        {
            if (request.BasketId.HasValue)
            {
                return await _basketStore.GetAsync(request.BasketId.Value);
            }

            return null;
        }
    }
}
