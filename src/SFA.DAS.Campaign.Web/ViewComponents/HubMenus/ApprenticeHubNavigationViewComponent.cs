using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.ViewComponents.HubMenus
{
    public class ApprenticeHubNavigationViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly ISessionService _sessionService;

        public ApprenticeHubNavigationViewComponent(IContentService contentService, ISessionService sessionService)
        {
            _contentService = contentService;
            _sessionService = sessionService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navBar = await _contentService.GetNavigationFor(HubTypes.Apprentice);
            
            return View(navBar);
        }
    }
}