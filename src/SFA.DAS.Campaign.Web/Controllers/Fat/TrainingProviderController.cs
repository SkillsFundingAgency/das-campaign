using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class TrainingProviderController : Controller
    {

        [Route("/trainingprovider/search")]
        public IActionResult Search(TrainingProviderSearchViewModel model)
        { 
            return RedirectPermanent("");
        }

        [Route("/trainingprovider/details")]
        public IActionResult Details(TrainingProviderDetailQueryViewModel model)
        {
            return RedirectPermanent("");
        }
        
        [Route("/trainingprovider/validate-postcode")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult ValidatePostcode(string postcode)
        {
            return RedirectPermanent("");
        }
    }

    public class TrainingProviderDetailQueryViewModel
    {
        public int Ukprn { get; set; }
        public string PostCode { get; set; }
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
