using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class GetTailoredApprenticeshipAdviceController : Controller
    {
        [HttpGet("employer/tailored-apprenticeship-advice")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInspire/GetTailoredApprenticeshipAdvice.cshtml");
        }
    }
}