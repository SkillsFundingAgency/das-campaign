using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices
{
    public interface IProviderSearchProvider
    {
        Task<ProviderSearchResult<ProviderSearchResultItem>> SearchProvidersByLocation(string apprenticeshipId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter, int orderBy = 0);

        Task<GroupedProviderSearchResult<GroupedProviderSearchResultItem>> SearchProvidersByLocationGroupByProvider(string apprenticeshipId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter);

        Task<ProviderNameSearchResultsAndPagination> SearchProviderNameAndAliases(string searchTerm, int page, int pageSize);

        Task<ProviderLocationsSearchResult> GetClosestLocations(string apprenticeshipId, long ukprn, Coordinate searchPoint);
    }
}