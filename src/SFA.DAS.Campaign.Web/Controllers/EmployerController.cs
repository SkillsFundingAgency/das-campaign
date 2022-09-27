using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Content.Queries;
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
        public async Task<IActionResult> CalculateApprenticeshipFunding(string slug1, string slug2, [FromQuery] bool preview)
        {
            slug1 = "are-you-ready-to-get-going";
            slug2 = "future-proof-your-business";
            //var routes = _repository.GetRoutes(); - get courses here for type ahead in calc
            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = slug1, Preview = true});
            var panel2 = _mediator.Send(new GetPanelQuery() {Slug = slug2, Preview = true});

            await Task.WhenAll(staticContent, panel1, panel2);

            return View(new ApprenticeshipFundingViewModel
            {
                //courses
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel
            });
        }
    }
}