using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class LandingPageController : Controller
    {
        [HttpGet("/apprentices/are-apprenticeships-right-for-me")]
        public IActionResult AreApprenticeshipsRightForMe()
        {
            return View("~/Views/LandingPages/AreApprenticeshipsRightForMe.cshtml");
        }
        
        [HttpGet("/apprentices/how-do-they-work")]
        public IActionResult HowDoTheyWork()
        {
            return View("~/Views/LandingPages/HowDoTheyWork.cshtml");
        }
        
        [HttpGet("/apprentices/getting-started")]
        public IActionResult GettingStarted()
        {
            return View("~/Views/LandingPages/GettingStarted.cshtml");
        }
    }
}