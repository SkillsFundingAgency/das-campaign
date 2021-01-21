using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Collections.Generic;

    public interface IProviderSearchService
    {
        Task<ProviderSearchResults> SearchProviders(string apprenticeshipId, string postCode, Pagination pagination, IEnumerable<string> deliveryModes, bool hasNonLevyContract, bool showNationalOnly, int orderBy = 0);
        Task<GroupedProviderSearchResults> SearchProvidersWithGroupedResults(string apprenticeshipId, string postCode, Pagination pagination, bool hasNonLevyContract, bool showNationalOnly);
    }
}
