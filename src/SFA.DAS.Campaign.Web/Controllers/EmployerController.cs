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

        [Route("how-much-is-it-going-to-cost")]
        public async Task<IActionResult> HowMuchIsItGoingToCost()
        {
            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
        [Route("the-right-apprenticeship")]
        public IActionResult TheRightApprenticeship()
        {
            return View();
        }
        [Route("choose-training-provider")]
        public IActionResult ChooseATrainingProvider()
        {
            return View();
        }
        [Route("hire-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return RedirectToAction("Index", "HiringAnApprentice");
        }
        [Route("preparing-and-monitoring")]
        public IActionResult PreparingAndMonitoring()
        {
            return View();
        }
        [Route("assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return RedirectToAction("Index", "EndPointAssessments");
        }
        
        [Route("benefits")]
        public async Task<IActionResult> Benefits()
        {
            var content = await _contentService.GetContentById<InfoPage>("6m46ydU0VFGF1Q2b5h4XmD");
            
            return View(content);
        }

        [Route("find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }
    }

    
}