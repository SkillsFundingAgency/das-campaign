using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    [Route("employer/funding-an-apprenticeship-non-levy")]
    public class FundingAnApprenticeshipNonLevyController : Controller
    {
        private readonly ISessionService _sessionService;

        public FundingAnApprenticeshipNonLevyController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var vm = 
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel();

            return View("~/Views/EmployerInform/FundingAnApprenticeshipNonLevy.cshtml", vm);
        }
        
        //
        // [HttpPost]
        // public IActionResult Index(LevyOptionViewModel vm)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        //     }
        //     _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);
        //     
        //     
        //
        //     return RedirectToAction("Index", "FundingAnApprenticeship");
        // }
    }
}
