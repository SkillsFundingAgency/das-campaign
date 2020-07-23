using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Application.Services;
using SFA.DAS.Campaign.Content;
using SFA.DAS.Campaign.Content.ContentTypes;

namespace SFA.DAS.Campaign.Web.ViewComponents.HubMenus
{
    public class EmployerHubNavigationViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly ISessionService _sessionService;

        public EmployerHubNavigationViewComponent(IContentService contentService, ISessionService sessionService)
        {
            _contentService = contentService;
            _sessionService = sessionService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navBar = _sessionService.Get<NavigationBar>("employerNavigationBar");
            if (!(navBar is null)) return View(navBar);
            
            navBar = await _contentService.GetNavigationFor(HubTypes.Employer);
            _sessionService.Set("employerNavigationBar", navBar);

            return View(navBar);
        }
    }
}