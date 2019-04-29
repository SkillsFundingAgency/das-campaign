using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class ApprenticeshipHeadingViewComponent : ViewComponent
    {
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;

        public ApprenticeshipHeadingViewComponent(IApprenticeshipOrchestrator apprenticeshipOrchestrator)
        {
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(ApprenticeshipDetailQueryViewModel queryModel)
        {

            var apprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(queryModel.Id);

            var model = new ApprenticeshipHeadingViewModel();

            switch (apprenticeshipType)
            {
                case ApprenticeshipType.Framework:
                    var framework = await _apprenticeshipOrchestrator.GetFramework(queryModel.Id);

                    model.Title = framework.Title;
                    model.Id = framework.Id;

                    break;
                case ApprenticeshipType.Standard:
                    break;
            }

            return View("Default", model);
        }
    }
}
