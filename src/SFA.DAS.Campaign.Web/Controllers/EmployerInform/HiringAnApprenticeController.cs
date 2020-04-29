﻿using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class HiringAnApprenticeController : Controller
    {
        [HttpGet("employer/hiring-an-apprentice")]
        public IActionResult Index()
        {
            return View("~/Views/EmployerInform/HiringAnApprentice.cshtml");
        }
    }
}