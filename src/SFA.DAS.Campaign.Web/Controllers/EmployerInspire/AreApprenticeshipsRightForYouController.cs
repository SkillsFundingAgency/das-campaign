using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class AreApprenticeshipsRightForYouController : Controller
    {
        public AreApprenticeshipsRightForYouController()
        {
            
        }

        [HttpGet("employer/are-apprenticeships-right-for-you")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInspire/AreApprenticeshipsRightForYou.cshtml");
        }
    }
}