using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class HowDoTheyWorkController : Controller
    {
        private readonly ISessionService _sessionService;
        
        public HowDoTheyWorkController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet("employer/how-do-they-work")]
        public IActionResult Index()
        {
            var vm = 
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel() {LevyStatus = LevyStatus.NonLevy};

            return View("~/Views/EmployerInform/HowDoTheyWork.cshtml", vm);
        }

        [HttpPost("employer/how-do-they-work")]
        public IActionResult Index(LevyOptionViewModel vm)
        {
            if (vm.LevyStatus != LevyStatus.DontKnow)
            {
                vm.PreviouslySet = true;    
            }
            
            _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);

            return RedirectToAction("Index", "HiringAnApprentice");
        }
    }
}