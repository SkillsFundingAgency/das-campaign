using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{hub}/{slug}")]
        public async Task<IActionResult> GetArticleAsync(string hub, string slug, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetArticleQuery
            {
                Hub = hub,
                Slug = slug
            }, cancellationToken).ConfigureAwait(false);

            if (result.Page == null)
            {
                return View("~/Views/Error/PageNotFound.cshtml");
            }

            return View($"~/Views/CMS/Article.cshtml", result.Page);
        }

        [HttpGet("/apprentices/benefits-apprenticeship")]
        public IActionResult BenefitsApprenticeship()
        {
            return View("~/Views/Articles/Apprentices/BenefitsApprenticeship.cshtml");
        }

        [HttpGet("/apprentices/help-shape-their-career")]
        public IActionResult HelpShapeTheirCareer()
        {
            return View("~/Views/Articles/Apprentices/HelpShapeTheirCareer.cshtml");
        }

        //[HttpGet("/apprentices/becoming-apprentice")]
        //public IActionResult BecomingAnApprentice()
        //{
        //    return View("~/Views/Articles/Apprentices/BecomingAnApprentice.cshtml");
        //}

        [HttpGet("/apprentices/applying-apprenticeship")]
        public IActionResult ApplyingForAnApprenticeship()
        {
            return View("~/Views/Articles/Apprentices/ApplyingForAnApprenticeship.cshtml");
        }

        [HttpGet("/apprentices/interview-process")]
        public IActionResult TheInterviewProcess()
        {
            return View("~/Views/Articles/Apprentices/TheInterviewProcess.cshtml");
        }

        [HttpGet("/apprentices/starting-apprenticeship")]
        public IActionResult StartingYourApprenticeship()
        {
            return View("~/Views/Articles/Apprentices/StartingYourApprenticeship.cshtml");
        }

        [HttpGet("/apprentices/assessment-and-certification")]
        public IActionResult AssessmentAndCertification()
        {
            return View("~/Views/Articles/Apprentices/AssessmentAndCertification.cshtml");
        }


        [HttpGet("/employers/benefits-of-hiring-apprentice")]
        public IActionResult BenefitsOfHiringApprentice()
        {
            return View("~/Views/Articles/Employers/BenefitsOfHiringApprentice.cshtml");
        }

        [HttpGet("/employers/hiring-an-apprentice")]
        public IActionResult HiringAnApprentice()
        {
            return View("~/Views/Articles/Employers/HiringAnApprentice.cshtml");
        }


        [HttpGet("/employers/upskilling-your-workforce")]
        public IActionResult UpskillingYourCurrentStaff()
        {
            return View("~/Views/Articles/Employers/UpskillingYourCurrentStaff.cshtml");
        }


        [HttpGet("/employers/training-your-apprentice")]
        public IActionResult TrainingYourApprentice()
        {
            return View("~/Views/Articles/Employers/TrainingYourApprentice.cshtml");
        }


        [HttpGet("/employers/end-point-assessments")]
        public IActionResult EndPointAssessments()
        {
            return View("~/Views/Articles/Employers/EndPointAssessments.cshtml");
        }


        [HttpGet("/employers/financial-incentives")]
        public IActionResult FinancialIncentives()
        {
            return View("~/Views/Articles/Employers/FinancialIncentives.cshtml");
        }

        [HttpGet("/employers/choose-apprenticeship-training")]
        public IActionResult ChooseRightApprenticeship()
        {
            return View("~/Views/Articles/Employers/ChooseRightApprenticeship.cshtml");
        }

        [HttpGet("/employers/choose-training-provider")]
        public IActionResult ChooseTrainingProvider()
        {
            return View("~/Views/Articles/Employers/ChooseTrainingProvider.cshtml");
        }


        [HttpGet("/employers/funding-an-apprenticeship-levy-payers")]
        public IActionResult FundingLevy()
        {
            return View("~/Views/Articles/Employers/FundingLevy.cshtml");
        }

        [HttpGet("/employers/funding-an-apprenticeship-non-levy")]
        public IActionResult FundingNonLevy()
        {
            return View("~/Views/Articles/Employers/FundingNonLevy.cshtml");
        }
    }
}