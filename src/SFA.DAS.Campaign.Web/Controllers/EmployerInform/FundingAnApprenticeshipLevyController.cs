﻿using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    [Route("employer/funding-an-apprenticeship-levy-payers")]
    public class FundingAnApprenticeshipLevyController : Controller
    {
        private readonly ISessionService _sessionService;

        public FundingAnApprenticeshipLevyController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var vm = 
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel();

            return View("~/Views/EmployerInform/FundingAnApprenticeshipLevyPayers.cshtml", vm);
        }
    }
}