using System.Threading.Tasks;
using Refit;
using Sfa.Das.FatApi.Client.Model.V4;

namespace Sfa.Das.FatApi.Client.Api
{
    public interface ISearchV4Api
    {
        [Get("/v4/apprenticeships/{id}/provider-locations/")]
        Task<V4GroupedProviderSearchResults> GetByApprenticeshipIdAndLatLon(string id, double lat, double lon, int page = 1, int pageSize = 20, bool showForNonLevyOnly = false, bool showNationalOnly = false);

        [Get("/v4/apprenticeships/{id}/provider/{ukprn}/locations/")]
        Task<V4ClosestLocationsSearchResults> GetClosestProviderLocationsThatCoverPointForApprenticeship(long ukprn, string id, double lat, double lon, bool showForNonLevyOnly = false, int page = 1, int pageSize = 5);
    }
}
