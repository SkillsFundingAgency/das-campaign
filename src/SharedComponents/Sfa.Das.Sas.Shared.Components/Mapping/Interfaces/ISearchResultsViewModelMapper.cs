using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ISearchResultsViewModelMapper
    {
        SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel> Map(GroupedProviderSearchResponse item, TrainingProviderSearchViewModel searchQueryModel);

    }
}
