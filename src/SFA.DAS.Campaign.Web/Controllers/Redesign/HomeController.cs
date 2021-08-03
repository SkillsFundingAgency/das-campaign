using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Campaign.Web.Helpers;
using Menu = SFA.DAS.Campaign.Domain.Content.Menu;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOptions<CampaignConfiguration> _configuration;

        public HomeController(IMediator mediator, IOptions<CampaignConfiguration> configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var menu = await _mediator.GetMenuForStaticContent();
            var banners = await _mediator.GetBannersForStaticContent();

            return View(menu);
        }

        [Route("sitemap")]
        public async Task<IActionResult> Sitemap()
        {
            var result = await _mediator.Send(new GetSiteMapQuery());

            return View(result.Page);
        }
        [Route("cookies")]
        public async Task<IActionResult> Cookies()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View(menu);
        }

        [Route("cookie-details")]
        public async Task<IActionResult> CookieDetails()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View(menu);
        }

        [Route("privacy")]
        public async Task<IActionResult> Privacy()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View(menu);
        }


        [Route("accessibility")]
        public async Task<IActionResult> Accessibility()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View("Accessibility", menu);
        }

        [Route("sitemap.xml")]
        public async Task<IActionResult> SiteMap()
        {
            var result = await _mediator.Send(new GetSiteMapQuery());

            var output = new StringBuilder();

            await GenerateXml(output, result, _configuration);

            var content = output.ToString();

            return new ContentResult
            {
                Content = content,
                ContentType = "application/xml",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        private static async Task GenerateXml(StringBuilder output, GetSiteMapQueryResult<SiteMap> result, IOptions<CampaignConfiguration> configuration)
        {
            using var xml = XmlWriter.Create(output, new XmlWriterSettings { Indent = true, Async = true });
            await xml.WriteStartDocumentAsync();
            xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            var baseUrl = configuration.Value.OuterApi.BaseUrl;

            foreach (var url in result.Page.Content.Urls)
            {
                xml.WriteStartElement("url");

                xml.WriteElementString("loc",
                    string.Compare(url.PageType, "hub", StringComparison.OrdinalIgnoreCase) == 0
                        ? $"{baseUrl}{url.Slug}"
                        : $"{baseUrl}{url.Hub}/{url.Slug}");

                await xml.WriteEndElementAsync();
            }

            await xml.WriteEndElementAsync();
            await xml.FlushAsync();
        }
    }
}