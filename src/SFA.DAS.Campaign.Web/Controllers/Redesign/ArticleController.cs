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
                Hub = hub, Slug = slug
            }, cancellationToken).ConfigureAwait(false);

            if (result.Page.Content == null)
            {
                return View("~/Views/Error/PageNotFound.cshtml");
            }
         
            return View($"~/Views/CMS/Article.cshtml", result);
        }
    }
}