using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("Employer")]
    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("How-Much-Is-It-Going-To-Cost")]
        public IActionResult HowMuchIsItGoingToCost()
        {
            return View();
        }

        public IActionResult TheRightApprenticeship()
        {
            return View();
        }

        public IActionResult ChooseATrainingProvider()
        {
            return View();
        }

        public IActionResult HireAnApprentice()
        {
            return View();
        }

        public IActionResult PreperationAndMonitoring()
        {
            return View();
        }

        public IActionResult AssessmentAndQualification()
        {
            return View();
        }

        public IActionResult WhatIsAnApprenticeship()
        {
            return View();
        }

        public IActionResult Benefits()
        {
            return View();
        }
    }
}