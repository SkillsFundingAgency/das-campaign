using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Shared.Components.Cookies;
using Sfa.Das.Sas.Shared.Components.ViewModels.Basket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.Controllers
{
    public class BasketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICookieManager _cookieManager;
        private readonly IBasketOrchestrator _basketOrchestrator;

        public BasketController(
            IMediator mediator,
            ICookieManager cookieManager,
            IBasketOrchestrator basketOrchestrator)
        {
            _mediator = mediator;
            _cookieManager = cookieManager;
            _basketOrchestrator = basketOrchestrator;
        }

        [HttpGet(Name = "BasketView")]
        public IActionResult View()
        {
            var vm = new BasketViewModel<ApprenticeshipBasketItemViewModel>();
            
            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                vm.AddRemoveBasketResponse = JsonConvert.DeserializeObject<AddOrRemoveFavouriteInBasketResponse>((string) TempData["AddRemoveResponse"]);
            }
            
            return View("Basket/View", vm);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveConfirmation(string apprenticeshipId, string returnPath)
        {
            var cookie = _cookieManager.Get(CookieNames.BasketCookie);
            Guid? cookieBasketId = Guid.TryParse(cookie, out Guid result) ? (Guid?)result : null;
            
            var basket = await _mediator.Send(new GetBasketQuery {BasketId = cookieBasketId.Value});
            var apprenticeshipItem = basket.SingleOrDefault(b => b.ApprenticeshipId == apprenticeshipId);
            
            return View("Basket/RemoveConfirmation", new RemoveConfirmationViewModel(apprenticeshipItem, returnPath));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveConfirmation(RemoveConfirmationViewModel vm)
        {
            var response = await _basketOrchestrator.UpdateBasket(vm.ApprenticeshipId);

            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                TempData.Remove("AddRemoveResponse");
            }
            
            TempData.Add("AddRemoveResponse", JsonConvert.SerializeObject(response));

            return Redirect(vm.ReturnPath);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddApprenticeshipFromDetails(SaveBasketFromApprenticeshipDetailsViewModel queryModel)
        {
            if (await HasLinkedTrainingProviders(queryModel.ItemId))
            {
                var returnPath = new Uri(Request.Headers["Referer"].ToString()).PathAndQuery;
                return RedirectToAction("RemoveConfirmation", "Basket", 
                    new
                    {
                        apprenticeshipId = queryModel.ItemId, returnPath
                    });
            }
            
            var response = await _basketOrchestrator.UpdateBasket(queryModel.ItemId);

            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                TempData.Remove("AddRemoveResponse");
            }
            
            TempData.Add("AddRemoveResponse", JsonConvert.SerializeObject(response));
            
            return RedirectToAction("Apprenticeship", "Fat", new { id = queryModel.ItemId });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddApprenticeshipFromResults(SaveBasketFromApprenticeshipResultsViewModel queryModel)
        {
            if (await HasLinkedTrainingProviders(queryModel.ItemId))
            {
                var returnPath = new Uri(Request.Headers["Referer"].ToString()).PathAndQuery;
                return RedirectToAction("RemoveConfirmation", "Basket", 
                    new
                    {
                        apprenticeshipId = queryModel.ItemId, returnPath
                    });
            }

            var response = await _basketOrchestrator.UpdateBasket(queryModel.ItemId);

            if (TempData.ContainsKey("AddRemoveResponse"))
            {
                TempData.Remove("AddRemoveResponse");
            }

            TempData.Add("AddRemoveResponse", JsonConvert.SerializeObject(response));
            
            return RedirectToAction("Search", "Fat", queryModel.SearchQuery);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddProviderFromDetails(SaveBasketFromProviderDetailsViewModel queryModel)
        {
            await _basketOrchestrator.UpdateBasket(queryModel.ApprenticeshipId, queryModel.Ukprn, queryModel.LocationIdToAdd);

            return RedirectToAction("Details", "TrainingProvider", queryModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddProviderFromResults(SaveBasketFromProviderSearchViewModel queryModel)
        {
            await _basketOrchestrator.UpdateBasket(queryModel.SearchQuery.ApprenticeshipId, queryModel.Ukprn,queryModel.LocationId);

            return RedirectToAction("Search", "TrainingProvider", queryModel.SearchQuery);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(DeleteFromBasketViewModel model)
        {
            // Check here if item being removed had TPs and is not a TP itself. If it has redirect to confirmation page.
            // else continue here as before.
            if (model.Ukprn == null)
            {
                if (await HasLinkedTrainingProviders(model.ApprenticeshipId))
                {
                    var returnPath = new Uri(Request.Headers["Referer"].ToString()).PathAndQuery;
                    return RedirectToAction("RemoveConfirmation", "Basket", 
                        new
                        {
                            apprenticeshipId = model.ApprenticeshipId, returnPath
                        });
                }
            }

            if (await IsInBasket(model.ApprenticeshipId, model.Ukprn))
            {
                await _basketOrchestrator.UpdateBasket(model.ApprenticeshipId, model.Ukprn);
            }

            return RedirectToAction("View", "Basket");
        }

        private async Task<bool> HasLinkedTrainingProviders(string apprenticeshipId)
        {
            var cookie = _cookieManager.Get(CookieNames.BasketCookie);
            Guid? cookieBasketId = Guid.TryParse(cookie, out Guid result) ? (Guid?)result : null;

            if (!cookieBasketId.HasValue) return false;
            
            var basket = await _mediator.Send(new GetBasketQuery {BasketId = cookieBasketId.Value});
            var apprenticeshipItem = basket.SingleOrDefault(b => b.ApprenticeshipId == apprenticeshipId);
            return apprenticeshipItem != null && apprenticeshipItem.Providers.Any();
        }

        private async Task<bool> IsInBasket(string apprenticeshipId, int? ukprn, int? locationId = null)
        {
            // Get cookie
            var cookie = _cookieManager.Get(CookieNames.BasketCookie);
            Guid? cookieBasketId = Guid.TryParse(cookie, out Guid result) ? (Guid?)result : null;

            if (cookieBasketId.HasValue)
            {
                var basket = await _mediator.Send(new GetBasketQuery { BasketId = cookieBasketId.Value });

                if (ukprn != null && locationId != null)
                {
                    return basket.IsInBasket(apprenticeshipId, ukprn.Value, locationId.Value);
                }
                return basket.IsInBasket(apprenticeshipId, ukprn);
            }

            return false;
        }

        public async Task<IActionResult> Save()
        {
            await _basketOrchestrator.DeleteBasketCache();

            return Redirect(await _basketOrchestrator.GetBasketSaveUrl());
        }

    }
}
