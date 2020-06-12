using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    [Route("employer/where-is-your-organisation-based")]
    public class WhereIsYourOrganisationBasedController : Controller
    {
        private readonly ISessionService _sessionService;

        public WhereIsYourOrganisationBasedController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            
            return View("~/Views/EmployerInspire/WhereIsYourOrganisationBased.cshtml", inspireJourneyChoices);
        }

        [HttpPost]
        public IActionResult Post(InspireJourneyChoices vm)
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();

            inspireJourneyChoices.Postcode = vm.Postcode;
            inspireJourneyChoices.Completed = true;
            
            _sessionService.Set(typeof(InspireJourneyChoices).Name, inspireJourneyChoices);

            return RedirectToAction("Index", "YourApprenticeshipAdvice");
        }
    }
}