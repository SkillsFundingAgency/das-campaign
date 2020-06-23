using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class YourApprenticeshipAdviceController : Controller
    {
        private readonly ISessionService _sessionService;

        public YourApprenticeshipAdviceController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name);

            if (inspireJourneyChoices is null || string.IsNullOrWhiteSpace(inspireJourneyChoices.Postcode))
            {
                return RedirectToAction("Index", "GetTailoredApprenticeshipAdvice");
            }
            
            return View("~/Views/EmployerInspire/YourApprenticeshipAdvice.cshtml", inspireJourneyChoices);
        }
    }
}