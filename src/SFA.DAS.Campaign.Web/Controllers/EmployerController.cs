using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Application.FundingTool;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;
using System.Threading.Tasks;
using Standard = SFA.DAS.Campaign.Application.FundingTool.Standard;

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
        public async Task<IActionResult> CalculateApprenticeshipFunding([FromQuery] bool preview)
        {
            var slug1 = "are-you-ready-to-get-going";
            var slug2 = "future-proof-your-business";
            var standards = _repository.GetStandards();
            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = slug1, Preview = preview });
            var panel2 = _mediator.Send(new GetPanelQuery() { Slug = slug2, Preview = preview });

            await Task.WhenAll(staticContent, panel1, panel2);

            return View(new ApprenticeshipFundingViewModel
            {
                Standards = standards.Result,
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel,
                Submitted = false
            });
        }

        [HttpPost("/employers/calculate-your-apprenticeship-funding")]
        public async Task<IActionResult> CalculateApprenticeshipFunding(ApprenticeshipFundingViewModel model)
        {
            var slug1 = "are-you-ready-to-get-going";
            var slug2 = "future-proof-your-business";
            var standardResult = _repository.GetStandard(model.StandardUid);
            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = slug1, Preview = true });
            var panel2 = _mediator.Send(new GetPanelQuery() { Slug = slug2, Preview = true });
            await Task.WhenAll(staticContent, panel1, panel2, standardResult);
            var standard = new Standard
            {
                Title = standardResult.Result.Title,
                StandardUId = standardResult.Result.StandardUId,
                LarsCode = standardResult.Result.LarsCode,
                Level = standardResult.Result.Level,
                MaxFunding = standardResult.Result.MaxFundingAvailable,
                Duration = standardResult.Result.TimeToComplete
            };
            CalculationOutputValues result = new CalculationInputValues()
            {
                PayBillGreaterThanThreeMillion = (bool)model.PayBillGreaterThanThreeMillion,
                OverFiftyEmployees = (bool)model.OverFiftyEmployees,
                TrainingCourse = standard,
                NumberRoles = model.NumberOfRoles
            }
            .CalculateFundingAndTraining();
            return View(new ApprenticeshipFundingViewModel
            {
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel,
                CalculationResults = new CalculationOutputValues
                {
                    Funding = result.Funding,
                    Training = result.Training == null ? 0 : result.Training,
                    Duration = result.Duration,
                    Title = result.Title,
                    Level = result.Level
                },
                Submitted = true
            });
        }
    }
}