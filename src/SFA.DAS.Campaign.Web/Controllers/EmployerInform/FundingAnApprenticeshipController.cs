using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;
using SFA.DAS.Campaign.Application.Services;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    [Route("employer/funding-an-apprenticeship")]
    public class FundingAnApprenticeshipController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IContentService _contentService;

        public FundingAnApprenticeshipController(ISessionService sessionService, IContentService contentService)
        {
            _sessionService = sessionService;
            _contentService = contentService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var levyOption  = 
                _sessionService.Get<LevyOptionViewModel>(_sessionService.LevyOptionViewModelKey) 
                ?? new LevyOptionViewModel() {LevyStatus = LevyStatus.NonLevy};

            var vm = new LevyQuestionDrivenViewModel();
            vm.LevyOptionViewModel = levyOption;
            
            var content = await _contentService.GetContentByHubAndSlug<InfoPage>(HubTypes.Employer, "funding-an-apprenticeship");

            vm.InfoPage = content;

            return View("~/Views/EmployerInform/FundingAnApprenticeship.cshtml", vm);
        }
        
        [HttpPost]
        public IActionResult Index(LevyOptionViewModel vm)
        {
            vm.OptionChosenByUser = true;

            _sessionService.Set(_sessionService.LevyOptionViewModelKey, vm);

            return RedirectToAction("Index", "FundingAnApprenticeship");
        }
    }

    public class LevyQuestionDrivenViewModel
    {
        public LevyOptionViewModel LevyOptionViewModel { get; set; }
        public InfoPage InfoPage { get; set; }
    }
}
