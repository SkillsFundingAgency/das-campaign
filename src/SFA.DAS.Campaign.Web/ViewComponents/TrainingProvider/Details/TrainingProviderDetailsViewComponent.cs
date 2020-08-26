using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using System;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.ApplicationServices.Services;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class TrainingProviderDetailsViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _tpOrchestrator;
        private readonly IApprenticeshipOrchestrator _apprenticeshipOrchestrator;
        private readonly ILog _logger;

        public TrainingProviderDetailsViewComponent(ITrainingProviderOrchestrator tpOrchestrator, IApprenticeshipOrchestrator apprenticeshipOrchestrator, ICacheStorageService cacheService, ILog logger)
        {
            _tpOrchestrator = tpOrchestrator;
            _apprenticeshipOrchestrator = apprenticeshipOrchestrator;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(TrainingProviderDetailQueryViewModel searchQueryModel, ViewType viewType = ViewType.Details)
        { 
           
            searchQueryModel.ApprenticeshipType = _apprenticeshipOrchestrator.GetApprenticeshipType(searchQueryModel.ApprenticeshipId);
            searchQueryModel.ViewType = viewType;

            
            var model = await _tpOrchestrator.GetDetails(searchQueryModel);

            model.SearchQuery = searchQueryModel;

            if (searchQueryModel.ApprenticeshipType == ApprenticeshipType.Standard)
            {
                model.Apprenticeship = await _apprenticeshipOrchestrator.GetStandard(searchQueryModel.ApprenticeshipId);
            }
            else
            {
                model.Apprenticeship = await _apprenticeshipOrchestrator.GetFramework(searchQueryModel.ApprenticeshipId);
            }

            switch (searchQueryModel.ViewType)
            {
                case ViewType.Contact:
                    return View("../TrainingProvider/Details/Contact", model);
                case ViewType.Summary:
                    return View("../TrainingProvider/Details/Summary", model);
                case ViewType.Details:
                default:
                    return View("../TrainingProvider/Details/Default", model);
            }

        }
    }
}
