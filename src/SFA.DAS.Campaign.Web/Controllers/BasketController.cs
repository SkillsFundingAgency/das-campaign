using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer/basket")]
    public class BasketController : Controller
    {
        [HttpGet]
        [Route("confirm-save")]
        public IActionResult Confirm()
        {
            return View();
        }
    }
}