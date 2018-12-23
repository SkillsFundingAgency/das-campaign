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
        [Route("what-are-the-benefits-for-me")]
        public IActionResult WhatAreTheBenefitsToMe()
        {
            return View();
        }
        [Route("application")]
        public IActionResult WhatAreTheStepsINeedToTake()
        {
            return View();
        }
        [Route("interview")]
        public IActionResult HowToGetYourApplicationRight()
        {
            return View();
        }
        [Route("your-apprenticeship")]
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