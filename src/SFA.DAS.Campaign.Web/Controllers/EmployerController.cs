using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("employer")]
    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("how-much-is-it-going-to-cost")]
        public IActionResult HowMuchIsItGoingToCost()
        {
            return View();
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
            return View();
        }
        [Route("preparing-and-monitoring")]
        public IActionResult PreparingAndMonitoring()
        {
            return View();
        }
        [Route("assessment-and-qualification")]
        public IActionResult AssessmentAndQualification()
        {
            return View();
        }
        [Route("what-is-an-apprenticeship")]
        public IActionResult WhatIsAnApprenticeship()
        {
            return View();
        }
        [Route("benefits")]
        public IActionResult Benefits()
        {
            return View();
        }
    }
}