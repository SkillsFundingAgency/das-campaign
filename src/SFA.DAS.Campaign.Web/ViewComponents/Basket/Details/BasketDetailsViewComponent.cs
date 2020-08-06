using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.Shared.Components.Cookies;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Shared.Components.Orchestrators;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Basket
{
    public class BasketDetailsViewComponent : ViewComponent
    {
        private readonly IBasketOrchestrator _basketOrchestrator;
        

        public BasketDetailsViewComponent(IBasketOrchestrator basketOrchestrator)
        {
            _basketOrchestrator = basketOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            // Get cookie
            var model = await _basketOrchestrator.GetBasket();


            return View("../Basket/Details/Default", model);
        }
    }
}
