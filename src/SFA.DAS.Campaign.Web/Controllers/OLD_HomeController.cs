using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class OldHomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("countries")]
        public IActionResult Countries()
        {
            return View("Countries");
        }

        [Route("thecalling")]
        public IActionResult TheCalling()
        {
            return RedirectToActionPermanent("Index");
        }
    }
}
