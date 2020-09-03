using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.Search;

namespace Sfa.Das.Sas.Shared.Components.Controllers
{
    public class FatController : Controller
    {
        [Route("/employer/find-apprenticeships")]
        public IActionResult Search(FatSearchViewModel model)
        {
            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                model.AddRemoveBasketResponse = 
                     JsonConvert.DeserializeObject<AddOrRemoveFavouriteInBasketResponse>((string)TempData["AddRemoveResponse"]);
            }
            
            return View("Fat/SearchResults", model);
        }

        [Route("/employer/find-apprenticeships/Apprenticeship")]
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
