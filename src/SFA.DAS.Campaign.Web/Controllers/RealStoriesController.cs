using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class RealStoriesController : Controller
    {
        public IActionResult Apprentice()
        {
            return View();
        }
        public IActionResult Employer()
        {
            return View();
        }
        public IActionResult FamousFaces()
        {
            return View();
        }
    }
}