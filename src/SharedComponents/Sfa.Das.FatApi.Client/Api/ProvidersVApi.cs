using System.Threading.Tasks;
using Refit;
using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.FatApi.Client.Api
{
    public interface IProvidersVApi
    {
        [Get("/v3/apprenticeships/{id}/providers")]
        Task<SFADASApprenticeshipsApiTypesV3ProviderApprenticeshipLocationSearchResult> GetByApprenticeshipIdAndLocationAsync(string id, double lat, double lon, int? page = null, int? pageSize = null, bool? showForNonLevyOnly = null, bool? showNationalOnly = null, string deliveryModes = null, int orderBy = 0);
    }
}
