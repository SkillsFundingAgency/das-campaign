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

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }

        [Route("accessibility")]
        public IActionResult Accessibility()
        {
            return View("Accessibility");
        }
    }
}