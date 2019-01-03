using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
      
            return View("Index");
        }
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("terms")]
        public IActionResult Terms()
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
    }
}
