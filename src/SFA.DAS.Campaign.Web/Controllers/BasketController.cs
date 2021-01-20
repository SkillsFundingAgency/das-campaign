using Microsoft.AspNetCore.Mvc;

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