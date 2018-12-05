using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SidebarType type, int activeIndex, string imgLocation)
        {
            string view;

            switch (type)
            {
                case SidebarType.Apprentice:
                    view = "../Shared/Sidebar/_Apprentice";
                    if (imgLocation == null)
                    {
                        imgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    break;
                case SidebarType.Employer:
                    view = "../Shared/Sidebar/_Employer";
                    if (imgLocation == null)
                    {
                        imgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    break;
                case SidebarType.None:
                    return Content(string.Empty);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return View("Default", new SidebarViewModel(type, view, activeIndex, imgLocation));
        }
    }

    public class SidebarViewModel
    {
        public SidebarViewModel(SidebarType type, string contentView, int activeIndex, string imgLocation)
        {
            Type = type;
            ActiveIndex = activeIndex;
            ImgLocation = imgLocation;
            ContentView = contentView;

        }

        public string ContentView { get; set; }

        public SidebarType Type { get; }

        public int ActiveIndex { get; set; }
        public string ImgLocation { get; set; }
        public string Title { get; set; }
    }

}