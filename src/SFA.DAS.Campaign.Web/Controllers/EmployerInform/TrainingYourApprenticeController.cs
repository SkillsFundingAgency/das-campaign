using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class TrainingYourApprenticeController : Controller
    {
        [HttpGet("/employer/training-your-apprentice")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/TrainingYourApprentice.cshtml");
        }
    }
}