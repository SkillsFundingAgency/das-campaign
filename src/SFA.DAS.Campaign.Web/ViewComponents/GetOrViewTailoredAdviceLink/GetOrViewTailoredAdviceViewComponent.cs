using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Controllers.EmployerInspire;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.ViewComponents.GetOrViewTailoredAdviceLink
{
    public class GetOrViewTailoredAdviceViewComponent : ViewComponent 
    {
        private readonly ISessionService _sessionService;

        public GetOrViewTailoredAdviceViewComponent(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        public IViewComponentResult Invoke()
        {
            var inspireJourneyChoices = _sessionService.Get<InspireJourneyChoices>(typeof(InspireJourneyChoices).Name);
            return View(!(inspireJourneyChoices is null) && inspireJourneyChoices.Completed);
        }
    }
}