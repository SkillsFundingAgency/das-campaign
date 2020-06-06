using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class UpskillController : Controller
    {
        [HttpGet("employer/upskill")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/Upskill.cshtml");
        }
    }
}