using System;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.SearchSummary
{
    public class TrainingProviderSearchSummaryViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _tpOrchestrator;
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;

        public TrainingProviderSearchSummaryViewComponent(ITrainingProviderOrchestrator tpOrchestrator, IApprenticeshipOrchestrator apprenticeshipOrchestrator)
        {
            _tpOrchestrator = tpOrchestrator;
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderSearchViewModel searchQueryModel, bool inline = false)
        {
            var apprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(searchQueryModel.ApprenticeshipId);
            var result = await _tpOrchestrator.GetSearchResults(searchQueryModel);

            var model = new TrainingProviderSearchSummaryViewModel
            {
                Postcode = searchQueryModel.Postcode,
                TotalResults = result.TotalResults,
            };

            switch (apprenticeshipType)
            {
                case ApprenticeshipType.Framework:
                    var framework = await _apprenticeshipOrchestrator.GetFramework(searchQueryModel.ApprenticeshipId);

                    model.ApprenticeshipTitle = framework.Title;
                    model.ApprenticeshipLevel = framework.Level;

                    break;
                case ApprenticeshipType.Standard:
                    var standard = await _apprenticeshipOrchestrator.GetStandard(searchQueryModel.ApprenticeshipId);

                    model.ApprenticeshipTitle = standard.Title;
                    model.ApprenticeshipLevel = standard.Level;
                    break;
            }

            switch (result.Status)
            {
                case ProviderSearchResponseCodes.ScotlandPostcode:
                   
                case ProviderSearchResponseCodes.WalesPostcode:
                case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                    return Content(string.Empty);
                default:
                    return View("../TrainingProvider/SearchSummary/Default", model);
            }

    
        }
    }
}
