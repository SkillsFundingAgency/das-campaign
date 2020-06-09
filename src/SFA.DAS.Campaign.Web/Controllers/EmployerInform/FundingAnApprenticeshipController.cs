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
                _sessionService.Get<LevyOption>(_sessionService.LevyOptionKey) 
                ?? new LevyOption() {LevyStatus = LevyStatus.NonLevy};

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public IActionResult Index(LevyOption vm)
        {
            vm.OptionChosenByUser = true;

            _sessionService.Set(_sessionService.LevyOptionKey, vm);

            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
    }
}
