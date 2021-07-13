using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Queries;

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
            var menu = await GetMenuForStaticContent(_mediator);

            return View(menu);
        }

        [Route("cookies")]
        public async Task<IActionResult> Cookies()
        {
            var menu = await GetMenuForStaticContent(_mediator);

            return View(menu);
        }

        [Route("cookie-details")]
        public async Task<IActionResult> CookieDetails()
        {
            var menu = await GetMenuForStaticContent(_mediator);

            return View(menu);
        }

        [Route("privacy")]
        public async Task<IActionResult> Privacy()
        {
            var menu = await GetMenuForStaticContent(_mediator);

            return View(menu);
        }


        [Route("accessibility")]
        public async Task<IActionResult> Accessibility()
        {
            var menu = await GetMenuForStaticContent(_mediator);

            return View("Accessibility", menu);
        }

        public static async Task<Page<Menu>> GetMenuForStaticContent(IMediator mediator)
        {
            var menu = await mediator.Send(new GetMenuQuery());

            return menu.Page;
        }
    }
}