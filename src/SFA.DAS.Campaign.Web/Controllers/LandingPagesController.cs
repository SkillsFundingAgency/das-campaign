using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class LandingPagesController : Controller
    {
        [Route("get-involved", Name = "ExpressionOfInterest")]
        public IActionResult ExpressionOfInterest()
        {
            return View();
        }

        [Route("get-involved/survey", Name = "ExpressionOfInterestSurvey")]
        public IActionResult ExpressionOfInterestSurvey()
        {
            return View();
        }

        [Route("get-involved/thank-you", Name = "ExpressionOfInterestThanks")]
        public IActionResult ExpressionOfInterestThank()
        {
            return View();
        }
    }
}