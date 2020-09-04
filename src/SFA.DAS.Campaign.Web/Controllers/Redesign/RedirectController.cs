using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.Redesign
{
    /// <summary>
    /// Controller purely for redirects based on historical urls and url changes.
    /// </summary>
    public class RedirectController : Controller
    {
        [Route("/apprentice/starting-apprenticeship")]
        [Route("/apprentice/your-apprenticeship")]
        public IActionResult YourApprenticeship()
        {
            return RedirectPermanent("/apprentices/starting-apprenticeship");
        }
        
        [Route("/apprentice/interview")]
        public IActionResult Interview()
        {
            return RedirectPermanent("/apprentices/interview-process");
        }
        
        [Route("/apprentice/application")]
        [Route("/apprentice/application-process")]
        public IActionResult ApplicationRedirect()
        {
            return RedirectPermanent("/apprentices/applying-apprenticeship");
        }
        
        [Route("/apprentice/what-is-apprenticeship")]
        [Route("/apprentice/what-is-an-apprenticeship")]
        public IActionResult WhatIsAnApprenticeshipRedirect()
        {
            return RedirectPermanent("/apprentices/what-is-apprenticeship");
        }
        
        [Route("/apprentice/what-are-the-benefits-for-me")]
        public IActionResult WhatAreTheBenefitsToMe()
        {
            return RedirectPermanent("/apprentices/benefits-apprenticeship");
        }
        
        [Route("/apprentice/assessment-and-certification")]
        public IActionResult AssessmentAndCertification()
        {
            return RedirectPermanent("/apprentices/assessment-and-certification");
        }
        
        [Route("/apprentice/find-an-apprenticeship")]
        public IActionResult FindAnApprenticeshipRedirect()
        {
            return RedirectToActionPermanent("FindAnApprenticeship", "Apprentice");
        }
        
        [Route("/employer/how-much-is-it-going-to-cost")]
        public IActionResult HowMuchIsItGoingToCost()
        {
            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
        
        [Route("/employer/the-right-apprenticeship")]
        public IActionResult TheRightApprenticeship()
        {
            return RedirectPermanent("/employers/the-right-apprenticeship");
        }
        
        [Route("/employer/choose-training-provider")]
        public IActionResult ChooseATrainingProvider()
        {
            return RedirectPermanent("/employers/choose-training-provider");
        }
        
        [Route("/employer/hire-an-apprentice")]
        [Route("/employer/hiring-an-apprentice")]
        public IActionResult HireAnApprentice()
        {
            return RedirectPermanent("/employers/hiring-an-apprentice");
        }
        
        [Route("/employer/end-point-assessments")]
        [Route("/employer/assessment-and-certification")]
        public IActionResult AssessmentAndQualification()
        {
            return RedirectPermanent("/employers/end-point-assessments");
        }
        
        [Route("/employer/benefits")]
        public IActionResult Benefits()
        {
            return RedirectPermanent("/employers/benefits-to-your-organisation");
        }
        
        [Route("/employer/find-apprenticeship-training")]
        public IActionResult FindApprenticeshipTrainingRedirect()
        {
            return RedirectToActionPermanent("FindApprenticeshipTraining", "Employer");
        }
        
        [Route("/employer/training-your-apprentice")]
        public IActionResult TrainingYourApprentice()
        {
            return RedirectPermanent("/employers/training-your-apprentice");
        }
     
        [Route("/employer/upskill")]
        public IActionResult Upskill()
        {
            return RedirectPermanent("/employers/upskill");
        }
        
        [Route("/real-stories/apprentice")]
        [Route("/apprentice/real-stories")]
        public IActionResult ApprenticeRedirect()
        {
            return RedirectToActionPermanent("Apprentices", "RealStories");
        }
        
        [Route("/real-stories/employer")]
        [Route("/employer/real-stories")]
        public IActionResult EmployerRedirect()
        {
            return RedirectToActionPermanent("Employers", "RealStories");
        }
        
        [Route("/parents/their-career")]
        public IActionResult TheirCareer(string email)
        {
            return RedirectPermanent("/employers/help-shape-their-career");
        }
    }
}