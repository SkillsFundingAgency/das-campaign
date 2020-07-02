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

        [Route("what-is-an-apprenticeship")]
        public async Task<IActionResult> WhatIsAnApprenticeship()
        {
            var applicationContent = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(applicationContent);
        }
        
        [Route("what-are-the-benefits-for-me")]
        public async Task<IActionResult> WhatAreTheBenefitsToMe()
        {
            var applicationContent = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(applicationContent);
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
        
        [Route("application")]
        public async Task<IActionResult> Application()
        {
            var content = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(content);
        }
        
        [Route("interview")]
        public async Task<IActionResult> Interview()
        {
            var content = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(content);
        }
        
        [Route("your-apprenticeship")]
        public async Task<IActionResult> YourApprenticeship()
        {
            var content = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(content);
        }
        
        [Route("assessment-and-certification")]
        public async Task<IActionResult> AssessmentAndQualification()
        {
            var content = await _contentService.GetContentBySlug<InfoPage>(HttpContext.Request.Path);
            
            return View(content);
        }
    }
}