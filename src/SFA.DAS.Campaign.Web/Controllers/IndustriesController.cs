using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class IndustriesController : Controller
    {
        
        [Route("/employers/browse-by-sector/")]
        public IActionResult EmployersSectors()
        {
            return View("Sectors");
        }

        [Route("/apprentices/browse-by-interests/")]
        public IActionResult ApprenticesInterests()
        {
            return View("Interests");
        }

        [Route("/employers/browse-by-sector/{slug}")]
        public IActionResult Sector(string slug)
        {
            return View($"~/Views/Industries/Employers/{slug}.cshtml");
        }
        
        [Route("/apprentices/browse-by-interests/{slug}")]
        public IActionResult Interest(string slug)
        {
            return View($"~/Views/Industries/Apprentices/{slug}.cshtml");
        }
    }
}