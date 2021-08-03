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
            var staticContent = await _mediator.GetModelForStaticContent();
            return View("Sectors", staticContent);
        }

        [Route("/apprentices/browse-by-interests/")]
        public async Task<IActionResult> ApprenticesInterests()
        {
            var staticContent = await _mediator.GetModelForStaticContent();
            return View("Interests", staticContent);
        }

        [Route("/employers/browse-by-sector/{slug}")]
        public async Task<IActionResult> Sector(string slug)
        {
            var staticContent = await _mediator.GetModelForStaticContent();

            return View($"~/Views/Industries/Employers/{slug}.cshtml", staticContent);
        }
        
        [Route("/apprentices/browse-by-interests/{slug}")]
        public async Task<IActionResult> Interest(string slug)
        {
            var staticContent = await _mediator.GetModelForStaticContent();

            return View($"~/Views/Industries/Apprentices/{slug}.cshtml", staticContent);
        }
    }
}