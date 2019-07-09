using Microsoft.AspNetCore.Mvc;

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

        [Route("sitemap")]
        public IActionResult Sitemap()
        {
            return View();
        }

        [Route("error/{errorCode}")]
        public IActionResult Error(string errorCode)
        {
            return View("Error", errorCode);
        }

      
    }
}
