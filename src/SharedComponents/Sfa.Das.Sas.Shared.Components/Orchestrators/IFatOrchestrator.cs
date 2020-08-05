using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public interface IFatOrchestrator
    {
        Task<FatSearchResultsViewModel> GetSearchResults(SearchQueryViewModel searchQueryModel);
        Task<FatSearchFilterViewModel> GetSearchFilters(SearchQueryViewModel searchQueryModel);
    }
}