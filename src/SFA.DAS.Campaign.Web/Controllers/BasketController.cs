using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer/basket")]
    public class BasketController : Controller
    {
        [HttpGet]
        [Route("confirm-save")]
        public IActionResult Confirm()
        {
            return View(new ConfirmSaveViewModel { ApprenticeshipId = "70", Ukprn = 10000028 });
        }
    }
}