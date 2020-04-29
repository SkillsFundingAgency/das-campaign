using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class EndPointAssessmentsController : Controller
    {
        [HttpGet("employer/end-point-assessments")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/EndPointAssessments.cshtml");
        }
    }
}