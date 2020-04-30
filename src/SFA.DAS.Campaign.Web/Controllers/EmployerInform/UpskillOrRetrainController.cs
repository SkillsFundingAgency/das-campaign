using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class UpskillOrRetrainController : Controller
    {
        [HttpGet("employer/upskill-or-retrain")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/UpskillOrRetrain.cshtml");
        }
    }
}