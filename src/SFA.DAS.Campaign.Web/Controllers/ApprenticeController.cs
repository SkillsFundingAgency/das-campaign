using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class ApprenticeController : Controller
    {
        private readonly IVacancyServiceApiHealthCheck _vacancyServiceApiHealthCheck;

        public ApprenticeController(IVacancyServiceApiHealthCheck healthCheck)
        {
            _vacancyServiceApiHealthCheck = healthCheck;
        }
        
        [Route("/apprentices/browse-apprenticeships")]
        public async Task<IActionResult> FindAnApprenticeship()
        {
            var health = await _vacancyServiceApiHealthCheck.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            // if (health.Status == HealthStatus.Unhealthy)
            // {
            //     return View("~/Views/Apprentice/RedirectToFAA.cshtml");
            // }

            return View();
        }
    }
}