using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("apprentice")]
    public class ApprenticeController : Controller
    {
        public IVacancyServiceApiHealthCheck _vacancyServiceApiHealthCheck;

        public ApprenticeController(IVacancyServiceApiHealthCheck healthCheck)
        {
            _vacancyServiceApiHealthCheck = healthCheck;
        }

        [Route("what-is-apprenticeship")]
        public IActionResult WhatIsAnApprenticeship()
        {
            return View();
        }
        [Route("what-is-an-apprenticeship")]
        public IActionResult WhatIsAnApprenticeshipRedirect()
        {
            return RedirectToActionPermanent("WhatIsAnApprenticeship");
        }

        [Route("what-are-the-benefits-for-me")]
        public IActionResult WhatAreTheBenefitsToMe()
        {
            return View();
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
        [Route("application-process")]
        public IActionResult Application()
        {
            return View();
        }
        [Route("application")]
        public IActionResult ApplicationRedirect()
        {
            return RedirectToActionPermanent("Application");
        }
        [Route("interview")]
        public IActionResult Interview()
        {
            return View();
        }
        [Route("starting-apprenticeship")]
        public IActionResult YourApprenticeship()
        {
            return View();
        }
        [Route("your-apprenticeship")]
        public IActionResult YourApprenticeshipRedirect()
        {
            return RedirectToActionPermanent("YourApprenticeship");
        }
        [Route("assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return View();
        }





    }
}