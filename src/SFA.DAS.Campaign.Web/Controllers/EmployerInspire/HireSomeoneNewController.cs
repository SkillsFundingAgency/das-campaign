using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    [Route("employer/hire-someone-new")]
    public class HireSomeoneNewController : Controller
    {
        private readonly ISessionService _sessionService;

        public HireSomeoneNewController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            
            return View("~/Views/EmployerInspire/HireSomeoneNew.cshtml", inspireJourneyChoices);
        }

        [HttpPost]
        public IActionResult Post(InspireJourneyChoices vm)
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();
            inspireJourneyChoices.HireSomeoneOptions = vm.HireSomeoneOptions;
            _sessionService.Set(typeof(InspireJourneyChoices).Name, inspireJourneyChoices);
            
            return RedirectToAction("Index", "WhereIsYourOrganisationBased");
        }
    }
}