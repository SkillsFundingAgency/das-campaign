using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Controllers.Redesign;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class IndustriesController : Controller
    {
        private readonly IMediator _mediator;

        public IndustriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/employers/browse-by-sector/")]
        public async Task<IActionResult> EmployersSectors()
        {
            var menu = await _mediator.GetMenuForStaticContent();
            return View("Sectors", menu);
        }

        [Route("/apprentices/browse-by-interests/")]
        public async Task<IActionResult> ApprenticesInterests()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View("Interests", menu);
        }

        [Route("/employers/browse-by-sector/{slug}")]
        public async Task<IActionResult> Sector(string slug)
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View($"~/Views/Industries/Employers/{slug}.cshtml", menu);
        }
        
        [Route("/apprentices/browse-by-interests/{slug}")]
        public async Task<IActionResult> Interest(string slug)
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View($"~/Views/Industries/Apprentices/{slug}.cshtml", menu);
        }
    }
}