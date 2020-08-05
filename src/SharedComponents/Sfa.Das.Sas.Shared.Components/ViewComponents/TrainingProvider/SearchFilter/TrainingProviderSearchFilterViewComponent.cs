using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class TrainingProviderSearchFilterViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _tpOrchestrator;

        public TrainingProviderSearchFilterViewComponent(ITrainingProviderOrchestrator tpOrchestrator)
        {
            _tpOrchestrator = tpOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderSearchViewModel searchQueryModel, bool inline = false)
        {

            var model = await _tpOrchestrator.GetSearchFilter(searchQueryModel);

            switch (model.Status)
            {
                case ProviderSearchResponseCodes.Success:
                    return View("../TrainingProvider/SearchFilter/Default", model);
                default:
                    return Content(string.Empty);
        
            }
        }
    }
}
