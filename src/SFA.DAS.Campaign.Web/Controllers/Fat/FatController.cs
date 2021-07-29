using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;

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
        [Route("/employer/find-apprenticeships")]
        public IActionResult Search(SearchQueryViewModel model)
        {
            var selectedLevels = "&levels=";
            if (model.SelectedLevels.Count < 7)
            {
                selectedLevels += string.Join("&levels=", model.SelectedLevels);
            }
            
            var url = $"{_configuration.FatBaseUrl}/courses?keyword={model.Keywords}{selectedLevels}";
            
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
