using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using System.Linq;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class SearchResultsViewModelMapper : ISearchResultsViewModelMapper
    {
        private readonly ITrainingProviderSearchResultsItemViewModelMapper _providerSearchResultsItemMaper;

        public SearchResultsViewModelMapper(ITrainingProviderSearchResultsItemViewModelMapper providerSearchResultsItemMaper)
        {
            _providerSearchResultsItemMaper = providerSearchResultsItemMaper;
        }


        public SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel> Map(GroupedProviderSearchResponse source, TrainingProviderSearchViewModel query)
        {
            source.SearchTerms = query.Postcode;

            var item = new SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>()
            {

                LastPage = source.Results?.LastPage ?? 0,
                SearchQuery = query,
                Status = source.StatusCode

            };

            if (source.Results != null)
            {

                item.SearchResults = source.Results.Hits?.Select(s => _providerSearchResultsItemMaper.Map(s));
                item.TotalResults = source.Results.TotalResults;

            }
            return item;
        }
    }
}
