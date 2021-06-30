using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class TrainingProviderController : Controller
    {
        private readonly CampaignConfiguration _configuration;

        public TrainingProviderController (IOptions<CampaignConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        
        [Route("/trainingprovider/search")]
        public IActionResult Search(TrainingProviderSearchViewModel model)
        {
            var url = _configuration.FatBaseUrl;
            
            if (!string.IsNullOrEmpty(model.ApprenticeshipId))
            {
                url += $"courses/{model.ApprenticeshipId}/providers?location={model.Postcode}";
            }
            return RedirectPermanent(url);
        }

        [Route("/trainingprovider/details")]
        public IActionResult Details(TrainingProviderDetailQueryViewModel model)
        {
            var url = _configuration.FatBaseUrl;
            
            if (!string.IsNullOrEmpty(model.ApprenticeshipId) && model.Ukprn != 0)
            {
                url += $"courses/{model.ApprenticeshipId}/providers/{model.Ukprn}?location={model.PostCode}";
            }
            
            return RedirectPermanent(url);
        }
        
        [Route("/trainingprovider/validate-postcode")]
        [HttpGet]
        public IActionResult ValidatePostcode(string postcode)
        {
            return RedirectPermanent(_configuration.FatBaseUrl);
        }
    }

    public class TrainingProviderDetailQueryViewModel
    {
        public int Ukprn { get; set; }
        public string PostCode { get; set; }
        public string ApprenticeshipId { get; set; }
    }
    
    public class SearchQueryViewModel
    {
        public string Keywords { get; set; }
        
        public List<int> SelectedLevels { get; set; } = new List<int>() { 2, 3, 4, 5, 6, 7, 8};
        
        
    }
    public class TrainingProviderSearchViewModel : SearchQueryViewModel
    {
        public string SearchRoute { get; set; } = "/TrainingProvider/Search";

        public string Postcode { get; set; }

        public string ApprenticeshipId { get; set; }

        public bool IsLevyPayer { get; set; }

        public bool NationalProvidersOnly { get; set; }
    }

}
