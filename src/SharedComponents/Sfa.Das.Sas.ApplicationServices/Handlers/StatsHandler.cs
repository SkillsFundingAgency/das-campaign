using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Core.Domain.Repositories;
using Sfa.Das.Sas.Core.Domain.Services;
using IGetProviders = Sfa.Das.Sas.Core.Domain.Services.IGetProviders;

namespace Sfa.Das.Sas.ApplicationServices.Handlers
{
    public class StatsHandler : IRequestHandler<StatsQuery, StatsResponse>
    {
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;
        private readonly IGetProviders _getProviders;
        private readonly IApprenticeshipProviderRepository _apprenticeshipProviderRepository;

        public StatsHandler(
            IGetStandards getStandards,
            IGetFrameworks getFrameworks,
            IGetProviders getProviders,
            IApprenticeshipProviderRepository apprenticeshipProviderRepository)
        {
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _getProviders = getProviders;
            _apprenticeshipProviderRepository = apprenticeshipProviderRepository;
        }

        public async Task<StatsResponse> Handle(StatsQuery message, CancellationToken cancellationToken)
        {
            var response = new StatsResponse
            {
                StandardCount = (int)_getStandards.GetStandardsAmount(),
                FrameworkCount = (int)_getFrameworks.GetFrameworksAmount(),
                ProviderCount = (int)_getProviders.GetProvidersAmount(),
                StandardOffer = (int)_getStandards.GetStandardsOffer(),
                FrameworkOffer = (int)_getFrameworks.GetFrameworksOffer(),
                UnpublishedStandardsCount = _getStandards.GetAllStandards().Count(s => s.IsPublished == false),
                ExpiringFrameworks30 = _getFrameworks.GetFrameworksExpiringSoon(30),
                ExpiringFrameworks90 = _getFrameworks.GetFrameworksExpiringSoon(90),
                StandardsWithProviders = _apprenticeshipProviderRepository.GetStandardsAmountWithProviders(),
                FrameworksWithProviders = _apprenticeshipProviderRepository.GetFrameworksAmountWithProviders()
            };

            response.StandardsWithoutProviders = response.StandardCount - response.StandardsWithProviders;
            response.FrameworksWithoutProviders = response.FrameworkCount - response.FrameworksWithProviders;

            return response;
        }
    }
}