using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class FatController : Controller
    {
        private readonly CampaignConfiguration _configuration;

        public FatController (IOptions<CampaignConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        
        [Route("/employers/find-apprenticeships")]
        public IActionResult Search(SearchQueryViewModel model)
        {
            var url = $"{_configuration.FatBaseUrl}/courses?keyword={model.Keywords}&levels={string.Join("&levels=",model.SelectedLevels)}";
            
            return RedirectPermanent(url);
        }

        [Route("/employers/find-apprenticeships/apprenticeship")]
        public IActionResult Apprenticeship(string id)
        {
            var url = $"{_configuration.FatBaseUrl}courses/{id}";
            return RedirectPermanent(url);
        }

    }
}
