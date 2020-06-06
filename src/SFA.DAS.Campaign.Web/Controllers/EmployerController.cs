using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer")]
    public class EmployerController : Controller
    {
        [Route("how-much-is-it-going-to-cost")]
        public IActionResult HowMuchIsItGoingToCost()
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
        public IActionResult Benefits()
        {
            return View();
        }

        [Route("find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTraining()
        {
            return View();
        }
    }
}