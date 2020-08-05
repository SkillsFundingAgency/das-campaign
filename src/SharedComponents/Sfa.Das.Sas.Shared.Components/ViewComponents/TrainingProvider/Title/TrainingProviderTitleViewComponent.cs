using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.SearchSummary;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class TrainingProviderTitleViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _tpOrchestrator;
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;

        public TrainingProviderTitleViewComponent(ITrainingProviderOrchestrator tpOrchestrator, IApprenticeshipOrchestrator apprenticeshipOrchestrator)
        {
            _tpOrchestrator = tpOrchestrator;
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderDetailQueryViewModel providerDetailsQueryModel, TrainingProviderSearchViewModel searchQueryModel, string title = "Training provider results")
        {
            if (providerDetailsQueryModel != null)
            {
                providerDetailsQueryModel.ApprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(providerDetailsQueryModel.ApprenticeshipId);
                var model = await _tpOrchestrator.GetDetails(providerDetailsQueryModel);

                model.SearchQuery = providerDetailsQueryModel;


                return View("../TrainingProvider/Title/Default", model.Name);
            }
            else
            {
                var result = await _tpOrchestrator.GetSearchResults(searchQueryModel);

             

                switch (result.Status)
                {
                    case ProviderSearchResponseCodes.ScotlandPostcode:
                        return View("../TrainingProvider/Title/Scotland");
                    case ProviderSearchResponseCodes.WalesPostcode:
                        return View("../TrainingProvider/Title/Wales");
                    case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                        return View("../TrainingProvider/Title/NorthernIreland");
                    case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                        return View("../TrainingProvider/Title/NonUK");
                    default:
                        return View("../TrainingProvider/Title/Default", title);
                }
            }

        }
    }
}   
