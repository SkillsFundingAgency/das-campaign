using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{

    public class LandingPagesController : Controller
    {
        [Route("eoi")]
        public IActionResult ExpressionOfInterest()
        {
            return View();
        }
        [Route("eoi/survey")]
        public IActionResult ExpressionOfInterestSurvey()
        {
            return View();
        }
        [Route("eoi/thanks")]
        public IActionResult ExpressionOfInterestThank()
        {
            return View();
        }
    }
}