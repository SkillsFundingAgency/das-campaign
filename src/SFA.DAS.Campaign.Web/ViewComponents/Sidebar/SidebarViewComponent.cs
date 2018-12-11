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
            string title;

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
            return View("Default", new SidebarViewModel(type, view,null, activeIndex, imgLocation));
        }
    }

    public class SidebarViewModel
    {
        public SidebarViewModel(SidebarType type, string contentView,string title, int activeIndex, string imgLocation)
        {
            Type = type;
            ActiveIndex = activeIndex;
            ImgLocation = imgLocation;
            ContentView = contentView;
            Title = title;

            switch (type)
            {
                case SidebarType.Apprentice:
                   

                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }

                    if (title == null)
                    {

                        Title = "the process to becoming an apprentice";
                    }

                    Classes = "sidebar-apprentice";
                    break;
                case SidebarType.Employer:
                   
                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    if (title == null)
                    {

                        Title = "the process to hire an apprentice";
                    }
                    Classes = "sidebar-apprentice";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

        }

        public string ContentView { get; set; }

        public SidebarType Type { get; }

        public int ActiveIndex { get; set; }
        public string ImgLocation { get; set; }
        public string Title { get; set; }
        public string Classes { get; set; }
    }

}