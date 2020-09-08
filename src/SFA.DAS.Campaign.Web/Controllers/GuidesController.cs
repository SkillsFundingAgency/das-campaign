using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class GuidesController : Controller
    {
        [Route("/employers/guides")]
        public IActionResult Index()
        {
            return View();
        }
    }
}