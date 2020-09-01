using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RealStoriesController : Controller
    {
        [Route("apprentice/real-stories")]
        public IActionResult Apprentice()
        {
            return RedirectToActionPermanent("Apprentices");
        }
        
        [Route("apprentices/real-stories")]
        public IActionResult Apprentices()
        {
            return View();
        }
        
        [Route("employer/real-stories")]
        public IActionResult Employer()
        {
            return RedirectToActionPermanent("Employers");
        }
        [Route("employers/real-stories")]
        public IActionResult Employers()
        {
            return View();
        }

        [Route("real-stories/apprentice")]
        public IActionResult ApprenticeRedirect()
        {
            return RedirectToActionPermanent("Apprentices");
        }
        [Route("real-stories/employer")]
        public IActionResult EmployerRedirect()
        {
            return RedirectToActionPermanent("Employers");
        }
    }
}