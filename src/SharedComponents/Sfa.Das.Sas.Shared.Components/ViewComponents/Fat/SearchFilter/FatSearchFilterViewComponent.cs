using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class FatSearchFilterViewComponent : ViewComponent
    {
        private readonly IFatOrchestrator _fatOrchestrator;

        public FatSearchFilterViewComponent(IFatOrchestrator fatOrchestrator)
        {
            _fatOrchestrator = fatOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchQueryViewModel searchQueryModel)
        {

            var model = await _fatOrchestrator.GetSearchFilters(searchQueryModel);

            return View("Default", model);

        }
    }
}
