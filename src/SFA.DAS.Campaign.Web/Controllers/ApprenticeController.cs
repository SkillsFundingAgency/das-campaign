using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ApprenticeController : Controller
    {
        [Route("/apprentices/browse-apprenticeships")]
        public IActionResult FindAnApprenticeship()
        {
           return View();
        }
    }
}