using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class HubController : Controller
    {
        [HttpGet("/apprentices")]
        public IActionResult Apprentices()
        {
            return View("~/Views/Hubs/ApprenticesHub.cshtml");
        }
        
        [HttpGet("/employers")]
        public IActionResult Employers()
        {
            return View("~/Views/Hubs/EmployersHub.cshtml");
        }
    }
}