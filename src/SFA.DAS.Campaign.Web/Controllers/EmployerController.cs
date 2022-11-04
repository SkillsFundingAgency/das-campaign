using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation;
using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId;
using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandards;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class EmployerController : Controller
    {
        private readonly CampaignConfiguration _configuration;
        private readonly IMediator _mediator;

        private const string calculationPanel1Slug = "are-you-ready-to-get-going";
        private const string calculationPanel2Slug = "future-proof-your-business";

        public EmployerController(IOptions<CampaignConfiguration> configuration, IMediator mediator)
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

        [Route("/employers/understanding-apprenticeship-benefits-and-funding")]
        public async Task<IActionResult> ApprenticeshipBenefitsAndFunding([FromQuery] bool preview = false)
        {
            var standards = _mediator.Send(new GetStandardsQuery());
            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = calculationPanel1Slug, Preview = preview });
            var panel2 = _mediator.Send(new GetPanelQuery() { Slug = calculationPanel2Slug, Preview = preview });

            await Task.WhenAll(standards, staticContent, panel1, panel2);

            return View(new ApprenticeshipTrainingAndBenefitsViewModel
            {
                Standards = standards.Result.Standards.Select(s => new Domain.Content.StandardResponse { Title = s.Title, LarsCode = s.LarsCode, Level = s.Level, StandardUId = s.StandardUId }).ToList(),
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel,
                Submitted = false
            });
        }

        [HttpPost("/employers/understanding-apprenticeship-benefits-and-funding")]
        public async Task<IActionResult> ApprenticeshipBenefitsAndFunding(ApprenticeshipTrainingAndBenefitsViewModel model, [FromQuery] bool preview = false)
        {
            if (model.PayBillGreaterThanThreeMillion == false)
            {
                if ((model.Roles > 10 || model.Roles < 1) && (model.Roles != null))
                {
                    ModelState.AddModelError("NumberOfRoles", "Enter a number between 1 and 10");
                }
            }
            else
            {
                if (model.Roles < 1)
                {
                    ModelState.AddModelError("NumberOfRoles", "Enter a number of roles");
                }
            }

            var staticContent = _mediator.GetModelForStaticContent();
            var panel1 = _mediator.Send(new GetPanelQuery() { Slug = calculationPanel1Slug, Preview = preview });
            var panel2 = _mediator.Send(new GetPanelQuery() { Slug = calculationPanel2Slug, Preview = preview });

            await Task.WhenAll(staticContent, panel1, panel2);

            if (!ModelState.IsValid)
            {
                var standards = await _mediator.Send(new GetStandardsQuery());
                model.Panel1 = panel1.Result.Panel;
                model.Panel2 = panel2.Result.Panel;
                model.Standards = standards.Standards.Select(s => new Domain.Content.StandardResponse { Title = s.Title, LarsCode = s.LarsCode, Level = s.Level, StandardUId = s.StandardUId }).ToList();
                model.Menu = staticContent.Result.Menu;
                model.BannerModels = staticContent.Result.BannerModels;
                return View("ApprenticeshipBenefitsAndFunding", model);
            }

            var standard = _mediator.Send(new GetStandardQuery { StandardUId = model.StandardUid });
            var calculationResult = _mediator.Send(new CalculationQuery() { PayBillGreaterThanThreeMillion = (bool)model.PayBillGreaterThanThreeMillion, OverFiftyEmployees = (bool)model.OverFiftyEmployees, NumberRoles = (int)model.Roles, TrainingCourse = standard });

            await Task.WhenAll(standard, calculationResult);

            return View(new ApprenticeshipTrainingAndBenefitsViewModel
            {
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels,
                Panel1 = panel1.Result.Panel,
                Panel2 = panel2.Result.Panel,
                CalculationResults = calculationResult.Result,
                Submitted = true,
                PayBillGreaterThanThreeMillion = model.PayBillGreaterThanThreeMillion,
                OverFiftyEmployees = model.OverFiftyEmployees,
                NumberOfRoles = model.NumberOfRoles
            });
        }
    }
}