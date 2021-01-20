using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class EmployerController : Controller
    {
        [Route("/employers/find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return RedirectPermanent("");
        }
    }
}