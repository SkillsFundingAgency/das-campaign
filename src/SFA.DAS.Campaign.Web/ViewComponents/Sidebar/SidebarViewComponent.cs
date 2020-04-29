using Microsoft.AspNetCore.Mvc;
using System;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(SidebarType? type, SidebarHeaderType? headerType, int activeIndex, string imgLocation, GoogleMapsViewModel googleMapsOptions, object formOptions)
        {
            string view;

            if (type == null)
            {

                switch (ViewContext.RouteData.Values["Controller"].ToString().ToLower())
                {
                    case "apprentice":
                        type = SidebarType.Apprentice;
                        break;
                    case "employer":
                        type = SidebarType.Employer;
                        break;
                    case "parents":
                        type = SidebarType.Parent;
                        break;
                    case "findapprenticeship":
                        type = SidebarType.Apprentice;
                        break;
                    case "findapprenticeshiptraining":
                        type = SidebarType.Employer;
                        break;
                    case "hiringanapprentice":
                    case "fundinganapprenticeship":
                    case "upskillorretrain":
                        type = SidebarType.EmployerWithoutNavigation;
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
                case SidebarType.EmployerWithoutNavigation:
                    view = "../Shared/Sidebar/_Employer-without-navigation";
                    break;
                case SidebarType.Parent:
                    view = "../Shared/Sidebar/_Parent";
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

            switch (headerType)
            {
                case SidebarHeaderType.GoogleMap:
                    return View("mapsSidebar", new SidebarViewModel((SidebarType)type, (SidebarHeaderType)headerType, view, activeIndex, imgLocation, googleMapsOptions));
                case SidebarHeaderType.Form:
                    return View("FormSidebar", new SidebarViewModel((SidebarType)type, (SidebarHeaderType)headerType, view, activeIndex, imgLocation, formModel: formOptions));

                default:
                    return View("Default", new SidebarViewModel((SidebarType)type, (SidebarHeaderType)headerType, view, activeIndex, imgLocation, googleMapsOptions));

            }

        }
    }


}