using Sfa.Das.FatApi.Client.Model;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    public interface IProviderNameSearchMapping
    {
        ProviderNameSearchResultsAndPagination Map(SFADASApprenticeshipsApiTypesV3ProviderSearchResults document, string searchTerm);
    }
}
