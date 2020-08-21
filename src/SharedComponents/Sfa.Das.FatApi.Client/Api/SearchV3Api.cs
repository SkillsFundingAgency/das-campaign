using System.Threading.Tasks;
using Refit;
using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.FatApi.Client.Api
{
    public interface ISearchV3Api
    {
        [Get("/v3/apprenticeship-programmes/search/")]
        Task<SFADASApprenticeshipsApiTypesV3ApprenticeshipSearchResults> SearchActiveApprenticeshipsAsync(string keywords, int? page = null, int? pageSize = null, int? order = null, string levels = null);

        [Get("/v3/providers/search/")]
        Task<SFADASApprenticeshipsApiTypesV3ProviderSearchResults> SearchProviderNameAsync(string keywords, int? page = null, int? pageSize = null);
    }
}
