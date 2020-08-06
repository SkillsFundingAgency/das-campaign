using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.Search;

namespace Sfa.Das.Sas.Shared.Components.Controllers
{
    public class FatController : Controller
    {
        public IActionResult Search(FatSearchViewModel model)
        {
            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                model.AddRemoveBasketResponse = 
                     JsonConvert.DeserializeObject<AddOrRemoveFavouriteInBasketResponse>((string)TempData["AddRemoveResponse"]);
            }
            
            return View("Fat/SearchResults", model);
        }

        public IActionResult Apprenticeship(string id)
        {
            var model = new ApprenticeshipDetailQueryViewModel(){Id = id};
            
            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                model.AddRemoveBasketResponse = 
                    JsonConvert.DeserializeObject<AddOrRemoveFavouriteInBasketResponse>((string)TempData["AddRemoveResponse"]);
            }
            
            return View("Fat/ApprenticeshipDetails", model);
        }

    }
}
