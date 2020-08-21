using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails
{
    public class ApprenticeshipDetailsViewComponent : ViewComponent
    {
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;

        public ApprenticeshipDetailsViewComponent(IApprenticeshipOrchestrator apprenticeshipOrchestrator)
        {
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(ApprenticeshipDetailQueryViewModel queryModel)
        {

            var apprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(queryModel.Id);


            switch (apprenticeshipType)
            {
                case ApprenticeshipType.Framework:
                    
                    return View("Framework",await _apprenticeshipOrchestrator.GetFramework(queryModel.Id));
                
                case ApprenticeshipType.Standard:
                    return View("Standard", await _apprenticeshipOrchestrator.GetStandard(queryModel.Id));
                    break;
            }
            return null;
        }
    }
}
