using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    [Route("real-stories")]
    public class RealStoriesController : Controller
    {
        [Route("apprentice")]
        public IActionResult Apprentice()
        {
            return View();
        }
        [Route("employer")]
        public IActionResult Employer()
        {
            return View();
        }
        [Route("famous-faces")]
        public IActionResult FamousFaces()
        {
            return View();
        }
    }
}