using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("cookies")]
        public IActionResult Cookies()
        {
            return View();
        }

        [Route("cookie-details")]
        public IActionResult CookieDetails()
        {
            return View();
        }
    }
}