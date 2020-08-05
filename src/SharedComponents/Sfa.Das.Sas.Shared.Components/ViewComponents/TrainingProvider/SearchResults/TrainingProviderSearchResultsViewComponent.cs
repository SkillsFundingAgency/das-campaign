using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class TrainingProviderSearchResultsViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _tpOrchestrator;

        public TrainingProviderSearchResultsViewComponent(ITrainingProviderOrchestrator tpOrchestrator)
        {
            _tpOrchestrator = tpOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderSearchViewModel searchQueryModel, bool inline = false)
        {

            var model = await _tpOrchestrator.GetSearchResults(searchQueryModel);


            switch (model.Status)
            {
                case ProviderSearchResponseCodes.Success:
                    return View("../TrainingProvider/SearchResults/Default", model);
                case ProviderSearchResponseCodes.ScotlandPostcode:
                    return View("../TrainingProvider/SearchResults/Scotland", model);
                case ProviderSearchResponseCodes.WalesPostcode:
                    return View("../TrainingProvider/SearchResults/Wales", model);
                case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                    return View("../TrainingProvider/SearchResults/NorthernIreland", model);
                case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                case ProviderSearchResponseCodes.PostCodeTerminated:
                    return View("../TrainingProvider/SearchResults/NonUK", model);
                default:
                    return Content(string.Empty);
            }
        }
    }
}
