using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

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