using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("real-stories")]
    public class RealStoriesController : Controller
    {
        [Route("apprentice")]
        public IActionResult Apprentice()
        {
            return View();
        }
        [Route("employer")]
        public IActionResult Employer()
        {
            return View();
        }
    }
}