using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class LandingPageController : Controller
    {
        [HttpGet("/apprentices/are-apprenticeships-right-for-me")]
        public IActionResult AreApprenticeshipsRightForMe()
        {
            return View("~/Views/LandingPages/Apprentices/AreApprenticeshipsRightForMe.cshtml");
        }
        
        [HttpGet("/apprentices/how-do-they-work")]
        public IActionResult HowDoTheyWork()
        {
            return View("~/Views/LandingPages/Apprentices/HowDoTheyWork.cshtml");
        }
        
        [HttpGet("/apprentices/getting-started")]
        public IActionResult GettingStarted()
        {
            return View("~/Views/LandingPages/Apprentices/GettingStarted.cshtml");
        }
        
        [HttpGet("/employers/are-apprenticeships-right-for-me")]
        public IActionResult EmployersAreApprenticeshipsRightForMe()
        {
            return View("~/Views/LandingPages/Employers/AreApprenticeshipsRightForMe.cshtml");
        }
        
        [HttpGet("/employers/how-do-they-work")]
        public IActionResult EmployersHowDoTheyWork()
        {
            return View("~/Views/LandingPages/Employers/HowDoTheyWork.cshtml");
        }
        
        [HttpGet("/employers/setting-it-up")]
        public IActionResult SettingItUp()
        {
            return View("~/Views/LandingPages/Employers/SettingItUp.cshtml");
        }
    }
}