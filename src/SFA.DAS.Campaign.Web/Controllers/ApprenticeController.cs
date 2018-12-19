using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("apprentice")]
    public class ApprenticeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("what-is-an-apprenticeship")]
        public IActionResult WhatIsAnApprenticeship()
        {
            return View();
        }
        [Route("what-are-the-benefits-to-me")]
        public IActionResult WhatAreTheBenefitsToMe()
        {
            return View();
        }
        [Route("what-are-the-steps-I-need-to-take")]
        public IActionResult WhatAreTheStepsINeedToTake()
        {
            return View();
        }
        [Route("how-to-get-your-application-right")]
        public IActionResult HowToGetYourApplicationRight()
        {
            return View();
        }
        [Route("monitor-your-progress")]
        public IActionResult MonitorYourProgress()
        {
            return View();
        }
        [Route("assessment-and-qualification")]
        public IActionResult AssessmentAndQualification()
        {
            return View();
        }


        


    }
}