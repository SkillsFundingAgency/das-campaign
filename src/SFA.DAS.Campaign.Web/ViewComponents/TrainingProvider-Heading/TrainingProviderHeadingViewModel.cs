using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.SearchResults;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class TrainingProviderHeadingViewModel
    {

        public ApprenticeshipHeadingViewModel Apprenticeship { get; set; } = new ApprenticeshipHeadingViewModel();
        public SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel> SearchResults { get; set;
        }

        public int Level { get; set; }
    }
}   
