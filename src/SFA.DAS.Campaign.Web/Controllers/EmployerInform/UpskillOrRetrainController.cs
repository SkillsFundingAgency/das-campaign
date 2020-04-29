using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class UpskillOrRetrain : Controller
    {
        [HttpGet("employer/upskill-or-retrain")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/UpskillOrRetrain.cshtml");
        }
    }
}