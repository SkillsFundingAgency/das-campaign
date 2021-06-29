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
        public async Task<IActionResult> GetArticleAsync(string hub, string slug, [FromQuery]bool preview, CancellationToken cancellationToken = default)
        {
            Page<Article> page;

            if (preview)
            {
                var result = await _mediator.Send(new GetArticlePreviewQuery
                {
                    Hub = hub,
                    Slug = slug
                }, cancellationToken).ConfigureAwait(false);

                page = result.Page;
            }
            else
            {
                var result = await _mediator.Send(new GetArticleQuery
                {
                    Hub = hub,
                    Slug = slug
                }, cancellationToken).ConfigureAwait(false);

                page = result.Page;
            }


            return page == null ? View("~/Views/Error/PageNotFound.cshtml") : View("~/Views/CMS/Article.cshtml", page);
        }
    }
}