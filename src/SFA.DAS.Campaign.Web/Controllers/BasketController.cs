using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer/basket")]
    public class BasketController : Controller
    {
        [HttpGet(Name = "BasketView")]
        public IActionResult View()
        {
            ApprenticeshipDetailQueryViewModel vm = null;

            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                vm = new ApprenticeshipDetailQueryViewModel()
                    {AddRemoveBasketResponse = JsonConvert.DeserializeObject<AddOrRemoveFavouriteInBasketResponse>((string) this.TempData["AddRemoveResponse"])};
            }

            return View("Basket/View", vm);
        }
        
        [HttpGet]
        [Route("confirm-save")]
        public IActionResult Confirm()
        {
            return View();
        }
    }
}