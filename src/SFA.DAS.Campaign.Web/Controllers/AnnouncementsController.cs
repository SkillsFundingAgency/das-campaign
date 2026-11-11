using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        public const string Hub = "employers";
        public const string Slug = "announcements";

        private readonly IMediator _mediator;

        public AnnouncementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/employers/announcements")]
        public async Task<IActionResult> Index([FromQuery] bool preview, CancellationToken cancellationToken = default)
        {
            var article = _mediator.Send(new GetArticleQuery { Hub = Hub, Slug = Slug, Preview = preview }, cancellationToken);
            var announcements = _mediator.Send(new GetAnnouncementsQuery { Preview = preview }, cancellationToken);

            await Task.WhenAll(article, announcements);

            if (article.Result.Page == null)
            {
                return await PageNotFound(cancellationToken);
            }

            var model = AnnouncementsPageViewModel.Build(article.Result.Page, announcements.Result.Announcements);

            return View("~/Views/Announcements/Index.cshtml", model);
        }

        [HttpGet("/employers/announcements/{slug}")]
        public async Task<IActionResult> Announcement(string slug, [FromQuery] bool preview, CancellationToken cancellationToken = default)
        {
            var announcement = _mediator.Send(new GetAnnouncementQuery { Slug = slug, Preview = preview }, cancellationToken);
            var article = _mediator.Send(new GetArticleQuery { Hub = Hub, Slug = Slug, Preview = preview }, cancellationToken);

            await Task.WhenAll(announcement, article);

            if (announcement.Result.Announcement == null)
            {
                return await PageNotFound(cancellationToken);
            }

            var parentPage = article.Result.Page;
            var menu = parentPage?.Menu;
            var banners = parentPage?.BannerModels;

            if (parentPage == null)
            {
                var staticContent = await _mediator.GetModelForStaticContent();
                menu = staticContent.Menu;
                banners = staticContent.BannerModels;
            }

            var model = AnnouncementPageViewModel.Build(announcement.Result.Announcement, parentPage, menu, banners);

            return View("~/Views/Announcements/Announcement.cshtml", model);
        }

        private async Task<IActionResult> PageNotFound(CancellationToken cancellationToken)
        {
            var sitemap = await _mediator.Send(new GetSiteMapQuery(), cancellationToken);

            Response.StatusCode = (int)HttpStatusCode.NotFound;

            return View("~/Views/Error/PageNotFound.cshtml", sitemap.Page);
        }
    }
}
