using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Application.FundingTool;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
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
        private readonly IStandardsRepository _repository;

        public EmployerController(IOptions<CampaignConfiguration> configuration, IMediator mediator, IStandardsRepository repository)
        {
            _configuration = configuration.Value;
            _mediator = mediator;
            _repository = repository;
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
            var standards = _repository.GetStandards(null);
            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = slug1, Preview = true });
            var panel2 = _mediator.Send(new GetPanelQuery() { Slug = slug2, Preview = true });

            await Task.WhenAll(staticContent, panel1, panel2);

            return View(new ApprenticeshipFundingViewModel
            {
                Standards = standards.Result,
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel
            });
        }

        [HttpPost("/employers/calculate-your-apprenticeship-funding")]
        public IActionResult CalculateApprenticeshipFunding(bool payBillGreaterThanThreeMillion, bool? overFiftyEmployees, Standard trainingCourse, int numberRoles)
        {
            var input = new CalculationInputValues() { PayBillGreaterThanThreeMillion = payBillGreaterThanThreeMillion, OverFiftyEmployees = overFiftyEmployees, TrainingCourse = trainingCourse, NumberRoles = numberRoles };
            var result = input.CalculateFundingAndTraining();
            return View(new ApprenticeshipFundingViewModel
            {
                Calculation = result
            });
        }
    }
}