using System.Threading.Tasks;
using Refit;
using Sfa.Das.FatApi.Client.Model;

namespace Sfa.Das.FatApi.Client.Api
{
    public interface ISearchApi
    {
        [Get("/apprenticeship-programmes/search/")]
        Task<SFADASApprenticeshipsApiTypesV2ApprenticeshipSearchResults> SearchActiveApprenticeships(string keywords, int? page = null, int? pageSize = null, int? order = null, string levels = null);
    }
}
