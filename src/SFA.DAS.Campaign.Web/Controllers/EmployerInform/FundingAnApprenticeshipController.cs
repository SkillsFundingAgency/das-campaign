﻿using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    [Route("employer/funding-an-apprenticeship")]
    public class FundingAnApprenticeshipController : Controller
    {
        private readonly ISessionService _sessionService;

        public FundingAnApprenticeshipController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var vm = 
                _sessionService.Get<LevyOptionViewModel>("LevyOptionViewModel") 
                ?? new LevyOptionViewModel() {LevyStatus = LevyStatus.NoneLevy};

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public IActionResult Index(LevyOptionViewModel vm)
        {
            vm.PreviouslySet = true;
            _sessionService.Set("LevyOptionViewModel", vm);

            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
    }
}
