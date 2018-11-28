using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers
{
    public class IndustriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}