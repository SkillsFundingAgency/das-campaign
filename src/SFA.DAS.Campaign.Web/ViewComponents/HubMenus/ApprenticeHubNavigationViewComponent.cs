using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Content;
using SFA.DAS.Campaign.Application.Content.ContentTypes;
using SFA.DAS.Campaign.Application.Services;
using SFA.DAS.Campaign.Infrastructure.Services;

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
            var navBar = _sessionService.Get<NavigationBar>("apprenticeNavigationBar");
            if (!(navBar is null)) return View(navBar);
            
            navBar = await _contentService.GetNavigationFor(HubTypes.Apprentice);
            _sessionService.Set("apprenticeNavigationBar", navBar);

            return View(navBar);
        }
    }
}