using Microsoft.AspNetCore.Mvc;
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
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel() {LevyStatus = LevyStatus.NonLevy};

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public IActionResult Index(LevyOptionViewModel vm)
        {
            if (vm.LevyStatus != LevyStatus.DontKnow)
            {
                vm.PreviouslySet = true;    
            }
            
            _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);

            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
    }
}
