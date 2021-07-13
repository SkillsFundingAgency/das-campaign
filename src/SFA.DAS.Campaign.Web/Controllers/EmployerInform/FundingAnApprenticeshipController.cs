﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    [Route("employers/funding-an-apprenticeship")]
    public class FundingAnApprenticeshipController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMediator _mediator;

        public FundingAnApprenticeshipController(ISessionService sessionService, IMediator mediator)
        {
            _sessionService = sessionService;
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = 
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel();

            var menu = await _mediator.GetMenuForStaticContent();

            vm.Menu = menu.Menu;

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public IActionResult Index(LevyOptionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
            }
            _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);

            return Redirect(vm.LevyStatus == LevyStatus.Levy ? "/employers/funding-an-apprenticeship-levy-payers" : "/employers/funding-an-apprenticeship-non-levy");
        }
    }
}
