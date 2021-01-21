using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Web.Controllers.Fat
{
    public class BasketController : Controller
    {
        private readonly CampaignConfiguration _configuration;

        public BasketController (IOptions<CampaignConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        
        [Route("/basket/view/")]
        public IActionResult View()
        {
            return RedirectPermanent(_configuration.FatBaseUrl);
        }

        [HttpGet("/basket/removeconfirmation")]
        public IActionResult RemoveConfirmation(string apprenticeshipId, string returnPath)
        {
            return RedirectPermanent(_configuration.FatBaseUrl);
        }

        [Route("/basket/save")]

        public IActionResult Save()
        {
            return RedirectPermanent(_configuration.FatBaseUrl);
        }

    }
}
