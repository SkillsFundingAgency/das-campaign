using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;
using SFA.DAS.Campaign.Web.Helpers;
using Menu = SFA.DAS.Campaign.Domain.Content.Menu;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View(menu);
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
    }
}