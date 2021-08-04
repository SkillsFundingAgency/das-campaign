using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Controllers.Redesign;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ApprenticeController : Controller
    {
        private readonly IStandardsRepository _repository;
        private readonly IMediator _mediator;

        public ApprenticeController(IStandardsRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [Route("/apprentices/employer-profiles")]
        public async Task<IActionResult> EmployerProfiles()
        {
            var menu = await _mediator.GetMenuForStaticContent();

            return View($"~/Views/Apprentice/EmployerProfiles.cshtml", menu);
        }

        [Route("/apprentices/browse-apprenticeships")]
        public async Task<IActionResult> FindAnApprenticeship()
        {
            var routes = await _repository.GetRoutes();
            var menu = await _mediator.GetMenuForStaticContent();
            
            return View(new FindApprenticeshipSearchModel
            {
                Routes = routes,
                Menu = menu.Menu
            });
        }
    }
}