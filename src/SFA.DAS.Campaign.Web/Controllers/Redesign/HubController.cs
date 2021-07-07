using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class HubController : Controller
    {
        private readonly IMediator _mediator;

        public HubController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{hub}")]
        public async Task<IActionResult> GetHubAsync(string hub, [FromQuery] bool preview, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetHubQuery
            {
                Hub = hub,
                Preview = preview
            }, cancellationToken).ConfigureAwait(false);

            var page = result.Page;

            return page == null ? View("~/Views/Error/PageNotFound.cshtml") : View($"~/Views/Hubs/{hub}Hub.cshtml", page);
        }

        [HttpGet("/influencers")]
        public IActionResult Influencers()
        {
            return View("~/Views/Hubs/InfluencersHub.cshtml");
        }
    }
}