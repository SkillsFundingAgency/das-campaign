using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class FatSearchResultsViewComponent : ViewComponent
    {
        private readonly IFatOrchestrator _fatOrchestrator;

        public FatSearchResultsViewComponent(IFatOrchestrator fatOrchestrator)
        {
            _fatOrchestrator = fatOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchQueryViewModel searchQueryModel, bool inline = false)
        {

            var model = await _fatOrchestrator.GetSearchResults(searchQueryModel);

            return View("Default", model);

        }
    }
}
