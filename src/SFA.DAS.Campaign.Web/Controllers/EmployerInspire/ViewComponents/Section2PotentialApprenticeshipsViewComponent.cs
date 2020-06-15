using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;
using Sfa.Das.FatApi.Client.Api;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.Orchestrators;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire.ViewComponents
{
    public class Section2PotentialApprenticeshipsViewComponent : ViewComponent
    {
        private readonly ISessionService _sessionService;
        private readonly ISearchV3Api _searchApi;
        private readonly IProviderSearchService _providerSearchService;

        public Section2PotentialApprenticeshipsViewComponent(ISessionService sessionService, ISearchV3Api searchApi, IProviderSearchService providerSearchService)
        {
            _sessionService = sessionService;
            _searchApi = searchApi;
            _providerSearchService = providerSearchService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name);

            foreach (var skill in inspireJourneyChoices.SelectedSkills)
            {
                var skillDescription = inspireJourneyChoices.Skills.Single(s => s.Key == skill).Title;
                var searchResult = await _searchApi.SearchActiveApprenticeshipsAsync(skillDescription);

                var firstSearchResult = searchResult.Results.First();

                var providersSearch = await _providerSearchService.SearchProviders(firstSearchResult.Id, inspireJourneyChoices.Postcode, new Pagination() {Page = 1, Take = 20}, new[] {""}, true, false, 0);

            }
            
            return View("~/Views/EmployerInspire/AdviceSections/_Section2.cshtml");
        }
    }
}