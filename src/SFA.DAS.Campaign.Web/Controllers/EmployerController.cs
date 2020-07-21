using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer")]
    public class EmployerController : Controller
    {
        private readonly IContentService _contentService;

        public EmployerController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        [Route("{slug}")]
        public async Task<IActionResult> ContentPage(string slug)
        {
            var applicationContent = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, slug);
            
            return View("~/Views/Employer/EmployerInfoPage.cshtml", applicationContent);
        }

        [Route("how-much-is-it-going-to-cost")]
        public async Task<IActionResult> HowMuchIsItGoingToCost()
        {
            return RedirectToActionPermanent("Index", "FundingAnApprenticeship");
        }
        
        [Route("hire-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return RedirectToActionPermanent("Index", "HiringAnApprentice");
        }

        [Route("assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return RedirectToActionPermanent("Index", "EndPointAssessments");
        }

        [Route("find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }
    }
}