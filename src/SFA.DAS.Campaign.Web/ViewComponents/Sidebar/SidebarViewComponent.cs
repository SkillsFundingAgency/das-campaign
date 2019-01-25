using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SidebarType? type, SidebarHeaderType? headerType, int activeIndex, string imgLocation, GoogleMapsViewModel googleMapsOptions)
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
                    case "FindApprenticeship":
                        type = SidebarType.Apprentice;
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

            if (headerType == null)
            {
                headerType = SidebarHeaderType.Image;
            }

            if (headerType == SidebarHeaderType.GoogleMap)
            {
                return View("DefaultNew", new SidebarViewModel((SidebarType)type, (SidebarHeaderType)headerType, view, activeIndex, imgLocation, googleMapsOptions));

            }
            return View("Default", new SidebarViewModel((SidebarType)type, (SidebarHeaderType)headerType, view, activeIndex, imgLocation, googleMapsOptions));
        }
    }


}