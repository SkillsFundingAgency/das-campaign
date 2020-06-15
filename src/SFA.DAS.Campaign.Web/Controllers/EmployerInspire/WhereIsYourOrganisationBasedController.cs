using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
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
            
            var vm = new InspirePostcodeViewModel();
            vm.Postcode = inspireJourneyChoices.Postcode;

            return View("~/Views/EmployerInspire/WhereIsYourOrganisationBased.cshtml", vm);
        }

        [HttpPost]
        public IActionResult Post(InspirePostcodeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/EmployerInspire/WhereIsYourOrganisationBased.cshtml", vm);
            }
            
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name) ?? new InspireJourneyChoices();

            inspireJourneyChoices.Postcode = vm.Postcode;
            inspireJourneyChoices.Completed = true;
            
            _sessionService.Set(typeof(InspireJourneyChoices).Name, inspireJourneyChoices);

            return RedirectToAction("Index", "YourApprenticeshipAdvice");
        }
    }

    public class InspirePostcodeViewModel
    {
        [Required(ErrorMessage = "Enter your organisation's postcode")]
        [RegularExpression(@"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})", ErrorMessage = "Enter a real postcode.")]
        public string Postcode { get; set; }
    }
}