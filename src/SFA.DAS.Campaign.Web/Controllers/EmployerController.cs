using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class EmployerController : Controller
    {
        private readonly CampaignConfiguration _configuration;
        private readonly IMediator _mediator;

        public EmployerController (IOptions<CampaignConfiguration> configuration, IMediator mediator)
        {
            _configuration = configuration.Value;
            _mediator = mediator;
        }
        
        [Route("/employers/find-apprenticeship-training")]
        [Route("/employer/find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return RedirectPermanent(_configuration.FatBaseUrl);
        }

        [Route("/employers/calculate-your-apprenticeship-funding")]
        public async Task<IActionResult> CalculateApprenticeshipFunding()
        {
            //var routes = _repository.GetRoutes();
            var staticContent = _mediator.GetModelForStaticContent();
            //panels - api call for each panel

            await Task.WhenAll(staticContent);

            return View(new ApprenticeshipFundingViewModel
            {
                //Routes = routes.Result,
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels
            });
        }
    }
}