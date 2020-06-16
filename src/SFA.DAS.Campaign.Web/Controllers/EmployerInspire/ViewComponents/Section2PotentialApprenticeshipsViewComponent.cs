using System;
using System.Collections.Generic;
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

            var apprenticeships = new List<ApprenticeshipViewModel>();
            
            foreach (var skill in inspireJourneyChoices.SelectedSkills)
            {
                var skillDescription = inspireJourneyChoices.Skills.Single(s => s.Key == skill).Title;
                var searchResult = await _searchApi.SearchActiveApprenticeshipsAsync(skillDescription);

                apprenticeships.AddRange(searchResult.Results.Select(sr => new ApprenticeshipViewModel{ Id = sr.Id, Name = sr.Title}));
            }

            apprenticeships.Shuffle(new Random());

            int numberOfResults = 0;
            
            
            
            
            
            var sixAtRandom = apprenticeships.Take(6).ToList();

            foreach (var randomApprenticeship in sixAtRandom)
            {
                var providersSearch = await _providerSearchService.SearchProviders(randomApprenticeship.Id, inspireJourneyChoices.Postcode, new Pagination() {Page = 1, Take = 100}, new[] {""}, true, false, 0);
                randomApprenticeship.NumberOfProviders = providersSearch.TotalResults;
            }

            return View("~/Views/EmployerInspire/AdviceSections/_Section2.cshtml", sixAtRandom);
        }
    }

    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for(var i=list.Count; i > 0; i--)
                list.Swap(0, rnd.Next(0, i));
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    
    public class ApprenticeshipViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long NumberOfProviders { get; set; }
    }
}