using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Helpers;
using Menu = SFA.DAS.Campaign.Domain.Content.Menu;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index([FromQuery] bool preview, CancellationToken cancellationToken = default)
        {
            var homepage = _mediator.Send(new GetHomepageQuery { Preview = preview }, cancellationToken);
            var staticContent = _mediator.GetModelForStaticContent();

            await Task.WhenAll(homepage, staticContent);

            var page = new Page<Homepage>
            {
                Content = homepage.Result.Homepage,
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels
            };

            return View(page);
        }

        [Route("sitemap")]
        public async Task<IActionResult> Sitemap()
        {
            var sitemap = _mediator.Send(new GetSiteMapQuery());
            var staticContent = _mediator.GetModelForStaticContent();

            await Task.WhenAll(sitemap, staticContent);

            sitemap.Result.Page.BannerModels = staticContent.Result.BannerModels;
            sitemap.Result.Page.Menu = staticContent.Result.Menu;

            return View(sitemap.Result.Page);
        }
        [Route("cookies")]
        public async Task<IActionResult> Cookies()
        {
            var page = await _mediator.GetModelForStaticContent();

            return View(page);
        }

        [Route("cookie-details")]
        public async Task<IActionResult> CookieDetails()
        {
            var page = await _mediator.GetModelForStaticContent();

            return View(page);
        }

        [Route("privacy")]
        public async Task<IActionResult> Privacy()
        {
            var page = await _mediator.GetModelForStaticContent();

            return View(page);
        }


        [Route("accessibility")]
        public async Task<IActionResult> Accessibility()
        {
            var page = await _mediator.GetModelForStaticContent();

            return View("Accessibility", page);
        }
    }
}