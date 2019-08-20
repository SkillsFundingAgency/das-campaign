using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer/basket")]
    public class BasketController : Controller
    {
        [HttpGet]
        [Route("confirm-save")]
        public IActionResult Confirm(string apprenticeshipId, int ukprn, int locationId)
        {
            return View(new ConfirmSaveViewModel
            {
                ApprenticeshipId = apprenticeshipId,
                Ukprn = ukprn,
                LocationId = locationId
            });
        }
    }
}