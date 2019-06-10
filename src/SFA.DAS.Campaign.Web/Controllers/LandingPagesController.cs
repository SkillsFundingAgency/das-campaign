using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class LandingPagesController : Controller
    {
        [Route("eoi", Name = "ExpressionOfInterest")]
        public IActionResult ExpressionOfInterest()
        {
            return View();
        }

        [Route("eoi/survey", Name = "ExpressionOfInterestSurvey")]
        public IActionResult ExpressionOfInterestSurvey()
        {
            return View();
        }

        [Route("eoi/thanks", Name = "ExpressionOfInterestThanks")]
        public IActionResult ExpressionOfInterestThank()
        {
            return View();
        }
    }
}