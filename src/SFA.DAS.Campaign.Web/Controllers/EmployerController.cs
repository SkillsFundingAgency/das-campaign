using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class EmployerController : Controller
    {
        private readonly IContentService _contentService;

        public EmployerController(IContentService contentService)
        {
            _contentService = contentService;
        }
        
        public async Task<IActionResult> ContentPage(string slug)
        {
            var applicationContent = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, slug);
            
            return View("~/Views/Employer/EmployerInfoPage.cshtml", applicationContent);
        }

        [Route("/employer/how-much-is-it-going-to-cost")]
        public async Task<IActionResult> HowMuchIsItGoingToCost()
        {
            return RedirectToActionPermanent("Index", "FundingAnApprenticeship");
        }
        
        [Route("/employer/hire-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return RedirectToActionPermanent("Index", "HiringAnApprentice");
        }
        
        [Route("/employer/assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return RedirectToActionPermanent("Index", "EndPointAssessments");
        }

        [Route("/employer/find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }
    }
}