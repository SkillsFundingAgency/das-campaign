using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.Cookies;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Basket
{
    public class AddOrRemoveFromBasketViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly ICookieManager _cookieManager;

        public AddOrRemoveFromBasketViewComponent(IMediator mediator, ICookieManager cookieManager)
        {
            _mediator = mediator;
            _cookieManager = cookieManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string apprenticeshipId, int? ukprn = null, int? locationId = null)
        {
            var model = new AddOrRemoveFromBasketViewModel
            {
                ItemId = ukprn.HasValue ? ukprn.ToString() : apprenticeshipId,
                LocationId = locationId,
                IsInBasket = await IsInBasket(apprenticeshipId, ukprn, locationId)
            };

            if (RouteData?.Values["Controller"]?.ToString().ToLower() == "basket")
            {
                return View("../Basket/AddOrRemoveFromBasket/Basket", model);
            }

            return View("../Basket/AddOrRemoveFromBasket/Default", model);
        }

        private async Task<bool> IsInBasket(string apprenticeshipId, int? ukprn, int? locationId)
        {
            // Get cookie
            var cookie = _cookieManager.Get(CookieNames.BasketCookie);
            Guid? cookieBasketId = Guid.TryParse(cookie, out Guid result) ? (Guid?)result : null;

            if (cookieBasketId.HasValue)
            {
                var basket = await _mediator.Send(new GetBasketQuery { BasketId = cookieBasketId.Value });

                if (locationId != null && ukprn != null)
                {
                    return basket.IsInBasket(apprenticeshipId, ukprn.Value, locationId.Value);
                }
                else
                {
                    return basket.IsInBasket(apprenticeshipId, ukprn);
                }
                
            }

            return false;
        }
    }
}
