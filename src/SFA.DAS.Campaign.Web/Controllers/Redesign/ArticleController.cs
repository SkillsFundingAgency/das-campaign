using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/apprentices/apprenticeships-alternatives")]
        public async Task<IActionResult> AlternativesToApprenticeships()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View("~/Views/Articles/Apprenticeships/AlternativesToApprenticeships.cshtml", menu);
        }

        [HttpGet("/employers/the-road-to-a-quality-apprenticeship")]
        public async Task<IActionResult> TheRoadToAQualityApprenticeship()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View("~/Views/Articles/Employers/TheRoadToAQualityApprenticeship.cshtml", menu);
        }

        [HttpGet("/influencers/alternatives-to-apprenticeships")]
        public async Task<IActionResult> AlternativesToApprenticeshipsInf()
        {
            var menu = await _mediator.GetMenuForStaticContent();
            
            return View("~/Views/Articles/Influencers/AlternativesToApprenticeships.cshtml", menu);
        }

        [HttpGet("/{hub}/{slug}")]
        public async Task<IActionResult> GetArticleAsync(string hub, string slug, [FromQuery]bool preview, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetArticleQuery
            {
                Hub = hub,
                Slug = slug,
                Preview = preview
            }, cancellationToken).ConfigureAwait(false);

            var page = result.Page;

            if (page != null)
            {
                return View("~/Views/CMS/Article.cshtml", page);
            }

            var landingPageResult = await _mediator.Send(new GetLandingPageQuery
            {
                Hub = hub,
                Slug = slug,
                Preview = preview
            }, cancellationToken).ConfigureAwait(false);

            var landingPage = landingPageResult.Page;

            if (landingPage == null)
            {
                var sitemap = await _mediator.Send(new GetSiteMapQuery(), cancellationToken);

                return View("~/Views/Error/PageNotFound.cshtml", sitemap.Page);
            }

            return View($"~/Views/LandingPages/{hub}LandingPage.cshtml", landingPage);
        }
    }
}