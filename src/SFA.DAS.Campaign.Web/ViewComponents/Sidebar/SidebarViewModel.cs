using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{

    public class SidebarViewModel
    {
        public SidebarViewModel(SidebarType type, string contentView, int activeIndex, string imgLocation)
        {
            Type = type;
            ActiveIndex = activeIndex;
            ImgLocation = imgLocation;
            ContentView = contentView;

            switch (type)
            {
                case SidebarType.Apprentice:
                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    Classes = "sidebar--apprentice";
                    break;
                case SidebarType.Employer:
                   
                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    Classes = "sidebar--employer";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

        }

        public string ContentView { get; set; }

        public SidebarType Type { get; }

        public int ActiveIndex { get; set; }
        public string ImgLocation { get; set; }
        public string Classes { get; set; }
    }

}