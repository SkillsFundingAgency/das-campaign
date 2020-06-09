using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class WhichSkillsController : Controller
    {
        private readonly ISessionService _sessionService;

        public WhichSkillsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("employer/which-skills")]
        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();

            return View("~/Views/EmployerInspire/WhichSkills.cshtml", inspireJourneyChoices);
        }

        [HttpPost("employer/which-skills")]
        public IActionResult SaveChanges(InspireJourneyChoices vm)
        {
            var storedInspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            storedInspireJourneyChoices.SelectedSkills = vm.SelectedSkills;
            _sessionService.Set(typeof(InspireJourneyChoices).Name, storedInspireJourneyChoices);

            return RedirectToAction("Index", "LevyNonLevy");
        }
    }
}