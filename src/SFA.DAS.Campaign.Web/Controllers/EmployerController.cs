using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewModels.CMS;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer")]
    public class EmployerController : Controller
    {
        private readonly IPageOrchestrator _pageOrchestrator;

        public EmployerController(IPageOrchestrator pageOrchestrator)
        {
            _pageOrchestrator = pageOrchestrator;
        }

        [Route("how-much-is-it-going-to-cost")]
        public async Task<IActionResult> HowMuchIsItGoingToCost()
        {
           return View("Page",await _pageOrchestrator.Get("how-much-will-it-cost"));
        }
        [Route("the-right-apprenticeship")]
        public async Task<IActionResult> TheRightApprenticeship()
        {
            return View("Page", await _pageOrchestrator.Get("the-right-apprenticeship"));
        }
        [Route("choose-training-provider")]
        public async Task<IActionResult> ChooseATrainingProvider()
        {
            return View("Page", await _pageOrchestrator.Get("choose-a-training-provider"));
        }
        [Route("hire-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return View();
        }
        [Route("preparing-and-monitoring")]
        public IActionResult PreparingAndMonitoring()
        {
            return View();
        }
        [Route("assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return View();
        }
        
        [Route("benefits")]
        public IActionResult Benefits()
        {
            return View();
        }

        [Route("find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }

        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return View("Page", await _pageOrchestrator.Get("matts-new-employer-page"));

        }
    }
}