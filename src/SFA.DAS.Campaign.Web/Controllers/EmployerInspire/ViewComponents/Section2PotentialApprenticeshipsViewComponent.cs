using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;
using Sfa.Das.Sas.Shared.Components.Orchestrators;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire.ViewComponents
{
    public class Section2PotentialApprenticeshipsViewComponent : ViewComponent
    {
        private readonly ISessionService _sessionService;
        private readonly IFatOrchestrator _fatOrchestrator;

        public Section2PotentialApprenticeshipsViewComponent(ISessionService sessionService, IFatOrchestrator fatOrchestrator)
        {
            _sessionService = sessionService;
            _fatOrchestrator = fatOrchestrator;
        }
        
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}