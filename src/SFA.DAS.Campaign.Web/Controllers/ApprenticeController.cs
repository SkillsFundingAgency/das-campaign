using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;

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
        public IActionResult FindAnApprenticeship()
        {
           return View();
        }
    }
}