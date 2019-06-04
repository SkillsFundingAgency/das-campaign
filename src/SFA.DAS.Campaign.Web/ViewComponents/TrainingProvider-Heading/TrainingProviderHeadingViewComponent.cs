using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class TrainingProviderHeadingViewComponent : ViewComponent
    {
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;
        private readonly ITrainingProviderOrchestrator _trainingProviderOrchestrator;

        public TrainingProviderHeadingViewComponent(IApprenticeshipOrchestrator apprenticeshipOrchestrator, ITrainingProviderOrchestrator trainingProviderOrchestrator)
        {
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
            _trainingProviderOrchestrator = trainingProviderOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderSearchViewModel queryModel)
        {
            var apprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(queryModel.ApprenticeshipId);

            var model = new TrainingProviderHeadingViewModel();

            model.SearchResults = await _trainingProviderOrchestrator.GetSearchResults(queryModel);

            switch (apprenticeshipType)
            {
                case ApprenticeshipType.Framework:
                    var framework = await _apprenticeshipOrchestrator.GetFramework(queryModel.ApprenticeshipId);

                    model.Apprenticeship.Title = framework.Title;
                    model.Apprenticeship.Id = framework.Id;
                    model.Apprenticeship.Level = framework.Level;

                    break;
                case ApprenticeshipType.Standard:
                    var standard = await _apprenticeshipOrchestrator.GetStandard(queryModel.ApprenticeshipId);

                    model.Apprenticeship.Id = standard.Id;
                    model.Apprenticeship.Title = standard.Title;
                    model.Apprenticeship.Overview = standard.Overview;
                    model.Apprenticeship.Level = standard.Level;
                    break;
            }

            return View("Default", model);
        }
    }
}
