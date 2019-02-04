using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SidebarType? type, int activeIndex, string imgLocation)
        {
            string view;
            string title;

            if (type == null)
            {
                
                switch (ViewContext.RouteData.Values["Controller"])
                {
                    case "Apprentice":
                        type = SidebarType.Apprentice;
                        break;
                    case "Employer":
                        type = SidebarType.Employer;
                        break;
                }
            }

            switch (type)
            {
                case SidebarType.Apprentice:
                    view = "../Shared/Sidebar/_Apprentice";
                    break;
                case SidebarType.Employer:
                    view = "../Shared/Sidebar/_Employer";

                    break;
                case SidebarType.None:
                    return Content(string.Empty);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return View("Default", new SidebarViewModel((SidebarType)type, view, activeIndex, imgLocation));
        }
    }
    

}