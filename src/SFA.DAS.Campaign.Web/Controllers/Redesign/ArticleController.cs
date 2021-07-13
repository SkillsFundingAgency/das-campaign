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

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/employers/the-road-to-a-quality-apprenticeship")]
        public async Task<IActionResult> TheRoadToAQualityApprenticeship()
        {
            var menu = await HomeController.GetMenuForStaticContent(_mediator);

            return View("~/Views/Articles/Employers/TheRoadToAQualityApprenticeship.cshtml", menu);
        }

        [Route("sitemap/xml")]
        public async Task<IActionResult> SiteMap()
        {
            var result = await _mediator.Send(new GetSiteMapQuery());

            var output = new StringBuilder();
           
            await GenerateXml(output, result);

            var content = output.ToString();

            return new ContentResult
            {
                Content = content,
                ContentType = "application/xml",
                StatusCode = (int)HttpStatusCode.OK
            };
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

            return landingPage == null ? View("~/Views/Error/PageNotFound.cshtml") : View($"~/Views/LandingPages/{hub}LandingPage.cshtml", landingPage);
        }

        private static async Task GenerateXml(StringBuilder output, GetSiteMapQueryResult<SiteMap> result)
        {
            using var xml = XmlWriter.Create(output, new XmlWriterSettings {Indent = true, Async = true});
            await xml.WriteStartDocumentAsync();
            xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            foreach (var url in result.Page.Content.Urls)
            {
                xml.WriteStartElement("url");

                xml.WriteElementString("loc",
                    string.Compare(url.PageType, "hub", StringComparison.OrdinalIgnoreCase) == 0
                        ? url.Slug
                        : $"{url.Hub}/{url.Slug}");

                await xml.WriteEndElementAsync();
            }

            await xml.WriteEndElementAsync();
            await xml.FlushAsync();
        }
    }
}