using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    
    public class FundingAnApprenticeship : Controller
    {
        [HttpGet("employer/funding-an-apprenticeship")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml");
        }
    }
}
