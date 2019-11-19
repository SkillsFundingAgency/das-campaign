using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("privacy")]
        public IActionResult Privacy()
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

        [Route("sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }
        [Route("countries")]
        public IActionResult Countries()
        {
            return View("Countries");
        }
        [Route("accessibility")]
        public IActionResult Accessibility()
        {
            return View("Accessibility");
        }

    }
}
