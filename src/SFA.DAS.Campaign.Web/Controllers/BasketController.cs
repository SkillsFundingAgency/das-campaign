using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Components.ViewModels;

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