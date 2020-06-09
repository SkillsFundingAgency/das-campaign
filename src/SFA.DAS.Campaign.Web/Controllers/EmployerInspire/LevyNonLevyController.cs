using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class LevyNonLevyController : Controller
    {
        private readonly ISessionService _sessionService;

        public LevyNonLevyController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet("employer/pay-bill")]
        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            
            return View("~/Views/EmployerInspire/LevyNonLevy.cshtml", inspireJourneyChoices);
        }
    }
}