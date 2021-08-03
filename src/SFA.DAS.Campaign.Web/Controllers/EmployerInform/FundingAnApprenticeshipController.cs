using System;
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

            var page = await _mediator.GetModelForStaticContent();

            vm.Menu = page.Menu;
            vm.BannerModels = page.BannerModels;

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(LevyOptionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var menu = await _mediator.GetMenuForStaticContent();
                vm.Menu = menu.Menu;
                return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
            }
            _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);

            return Redirect(vm.LevyStatus == LevyStatus.Levy ? "/employers/funding-an-apprenticeship-levy-payers" : "/employers/funding-an-apprenticeship-non-levy");
        }
    }
}
