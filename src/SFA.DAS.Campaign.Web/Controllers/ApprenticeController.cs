using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Web.Helpers;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ApprenticeController : Controller
    {
        private readonly IStandardsRepository _repository;
        private readonly IMediator _mediator;
        private readonly ILogger<ApprenticeController> _logger;

        public ApprenticeController(IStandardsRepository repository, IMediator mediator, ILogger<ApprenticeController> logger)
        {
            _repository = repository;
            _mediator = mediator;
            _logger = logger;
        }

        [Route("/apprentices/browse-apprenticeships")]
        public async Task<IActionResult> FindAnApprenticeship()
        {
            _logger.LogInformation("FindAnApprenticeship method invoked");

            var routes = _repository.GetRoutes();
            var staticContent = _mediator.GetModelForStaticContent();

            await Task.WhenAll(routes, staticContent);
            
            return View(new FindApprenticeshipSearchModel
            {
                Routes = routes.Result,
                Menu = staticContent.Result.Menu,
                BannerModels = staticContent.Result.BannerModels
            });
        }
    }
}