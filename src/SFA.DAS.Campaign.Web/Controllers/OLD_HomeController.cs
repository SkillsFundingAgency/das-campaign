using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class OldHomeController : Controller
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
        [Route("thecalling")]
        public IActionResult TheCalling()
        {
            return RedirectToActionPermanent("Index");
        }
    }
}
