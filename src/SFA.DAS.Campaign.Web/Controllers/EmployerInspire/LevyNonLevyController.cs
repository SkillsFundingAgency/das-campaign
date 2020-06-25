using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    [Route("employer/pay-bill")]
    public class LevyNonLevyController : Controller
    {
        private readonly ISessionService _sessionService;

        public LevyNonLevyController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            
            return View("~/Views/EmployerInspire/LevyNonLevy.cshtml", inspireJourneyChoices);
        }

        [HttpPost]
        public IActionResult Post(InspireJourneyChoices vm)
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            inspireJourneyChoices.LevyOption.LevyStatus = vm.LevyOption.LevyStatus;
            _sessionService.Set(typeof(InspireJourneyChoices).Name, inspireJourneyChoices);

            inspireJourneyChoices.LevyOption.OptionChosenByUser = true;
            _sessionService.Set(_sessionService.LevyOptionKey, inspireJourneyChoices.LevyOption);
            
            return RedirectToAction("Index", "HireSomeoneNew");
        }
    }
}