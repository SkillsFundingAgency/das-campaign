using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.SearchFilter;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.SearchResults;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public interface IFatOrchestrator
    {
        Task<FatSearchResultsViewModel> GetSearchResults(SearchQueryViewModel searchQueryModel);
        Task<FatSearchFilterViewModel> GetSearchFilters(SearchQueryViewModel searchQueryModel);
    }
}