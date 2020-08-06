using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.SearchResults;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ISearchResultsViewModelMapper
    {
        SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel> Map(GroupedProviderSearchResponse item, TrainingProviderSearchViewModel searchQueryModel);

    }
}
