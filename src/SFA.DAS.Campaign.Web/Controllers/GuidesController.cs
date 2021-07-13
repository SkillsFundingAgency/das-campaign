using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class GuidesController : Controller
    {
        private readonly IMediator _mediator;

        public GuidesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/employers/employer-guides")]
        public async Task<IActionResult> Index()
        {
            var menu = await _mediator.GetMenuForStaticContent();
            return View(menu);
        }
    }
}