using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.Orchestrators;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.Fat.SearchResults
{
    public class ClosestLocationsViewComponent : ViewComponent
    {
        private readonly ITrainingProviderOrchestrator _trainingProviderOrchestrator;

        public ClosestLocationsViewComponent(ITrainingProviderOrchestrator trainingProviderOrchestrator)
        {
            _trainingProviderOrchestrator = trainingProviderOrchestrator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string apprenticeshipId, int ukprn, int locationId, string postCode)
        {
            var model = await _trainingProviderOrchestrator.GetClosestLocations(apprenticeshipId, ukprn, locationId, postCode);
            
            return View("../TrainingProvider/ClosestLocations/Default", model);
        }
    }
}
