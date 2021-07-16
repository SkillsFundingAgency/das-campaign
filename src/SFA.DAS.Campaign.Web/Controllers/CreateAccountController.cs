using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Models.Configuration;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class CreateAccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CampaignConfiguration _configuration;

        public CreateAccountController(IOptions<CampaignConfiguration> configuration, IMediator mediator)
        {
            _mediator = mediator;
            _configuration = configuration.Value;
        }

        [Route("/apprentices/create-account")]
        public async Task<IActionResult> Apprentices()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View(menu);
        }
        
        [Route("/employers/create-apprenticeship-service-account")]
        public async Task<IActionResult> Employers()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            var viewModel = new CreateAccountModel
            {
                BaseEmployerAccountUrl = _configuration.EmployerAccountBaseUrl,
                Menu = menu.Menu
            };

            return View(viewModel);
        }
    }
}