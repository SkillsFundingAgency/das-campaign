using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Models.Configuration;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class CreateAccountController : Controller
    {

        private readonly CampaignConfiguration _configuration;

        public CreateAccountController(IOptions<CampaignConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        [Route("/apprentices/create-account")]
        public IActionResult Apprentices()
        {
            return View();
        }
        
        [Route("/employers/create-apprenticeship-service-account")]
        public IActionResult Employers()
        {
            var viewModel = new CreateAccountModel
            {
                BaseEmployerAccountUrl = _configuration.EmployerAccountBaseUrl
            };

            return View(viewModel);
        }
    }
}