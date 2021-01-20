using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class BasketController : Controller
    {

        [Route("/basket/view/")]
        public IActionResult View()
        {
            return RedirectPermanent("");
        }

        [HttpGet("/basket/removeconfirmation")]
        public IActionResult RemoveConfirmation(string apprenticeshipId, string returnPath)
        {
            return RedirectPermanent("");
        }

        [Route("/basket/save")]

        public IActionResult Save()
        {
            return RedirectPermanent("");
        }

    }
}
