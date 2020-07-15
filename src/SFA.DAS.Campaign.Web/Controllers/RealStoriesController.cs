using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RealStoriesController : Controller
    {
        [Route("/apprentice/real-stories")]
        public IActionResult Apprentice()
        {
            return View();
        }
        [Route("/employer/real-stories")]
        public IActionResult Employer()
        {
            return View();
        }
    }
}