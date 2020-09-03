using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class OldHomeController : Controller
    {
        [Route("countries")]
        public IActionResult Countries()
        {
            return View("Countries");
        }
    }
}
