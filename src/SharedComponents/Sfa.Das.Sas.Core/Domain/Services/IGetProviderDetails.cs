using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Apprenticeships.Api.Types.Providers;
using Provider = SFA.DAS.Apprenticeships.Api.Types.Providers.Provider;

namespace Sfa.Das.Sas.Core.Domain.Services
{
    public interface IGetProviderDetails
    {
        IEnumerable<ProviderSummary> GetAllProviders();
        Task<Provider> GetProviderDetails(long ukPrn);
        Task<ApprenticeshipTrainingSummary> GetApprenticeshipTrainingSummary(long ukprn, int page);
    }
}