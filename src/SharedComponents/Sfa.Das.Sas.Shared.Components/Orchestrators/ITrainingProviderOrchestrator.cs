using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.ClosestLocations;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Details;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.SearchFilter;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.SearchResults;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public interface ITrainingProviderOrchestrator
    {
        Task<SearchResultsViewModel<TrainingProviderSearchResultsItem,TrainingProviderSearchViewModel>> GetSearchResults(TrainingProviderSearchViewModel searchQueryModel);
        Task<TrainingProviderSearchFilterViewModel> GetSearchFilter(TrainingProviderSearchViewModel searchQueryModel);
        Task<TrainingProviderDetailsViewModel> GetDetails(TrainingProviderDetailQueryViewModel detailsQueryModel);
        Task<ClosestLocationsViewModel> GetClosestLocations(string apprenticeshipId, int ukprn, int locationId, string postCode);
    }
}