using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RealStoriesController : Controller
    {
        [Route("apprentices/real-stories")]
        public IActionResult Apprentices()
        {
            return View();
        }

        [Route("employers/real-stories")]
        public IActionResult Employers()
        {
            return View();
        }
    }
}