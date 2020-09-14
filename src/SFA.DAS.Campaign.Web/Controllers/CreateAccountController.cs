using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class CreateAccountController : Controller
    {
        [Route("/apprentices/create-account")]
        public IActionResult Apprentices()
        {
            return View();
        }
        
        [Route("/employers/create-apprenticeship-service-account")]
        public IActionResult Employers()
        {
            return View();
        }
    }
}