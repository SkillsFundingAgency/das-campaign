using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("apprentice")]
    
    public class ApprenticeController : Controller
    {
        private readonly IVacancyServiceApiHealthCheck _vacancyServiceApiHealthCheck;
        private readonly IContentService _contentService;

        public ApprenticeController(IVacancyServiceApiHealthCheck healthCheck, IContentService contentService)
        {
            _vacancyServiceApiHealthCheck = healthCheck;
            _contentService = contentService;
        }

        [Route("{slug}")]
        public async Task<IActionResult> ContentPage(string slug)
        {
            var applicationContent = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Apprentice, slug);
            
            return View("~/Views/Apprentice/ApprenticeInfoPage.cshtml", applicationContent);
        }
        
        [Route("find-an-apprenticeship")]
        public async Task<IActionResult> FindAnApprenticeship()
        {
            var health = await _vacancyServiceApiHealthCheck.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            if (health.Status == HealthStatus.Unhealthy)
            {
                return View("~/Views/Apprentice/RedirectToFAA.cshtml");
            }

            return View();
        }
    }
}