using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat
{
    public class FatSearchResultsViewModel : SearchResultsViewModel<ApprenticeshipItemViewModel,SearchQueryViewModel>
    {
        public FatSearchResultsViewModel()
        {
            SearchQuery = new SearchQueryViewModel();
        }
        public bool IsAllSearch => string.IsNullOrEmpty(SearchQuery.Keywords) || SearchQuery.Keywords == "*";
    }
}
