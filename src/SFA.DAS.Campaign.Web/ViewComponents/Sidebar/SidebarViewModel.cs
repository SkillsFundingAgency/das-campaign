using System;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;

namespace SFA.DAS.Campaign.Web.ViewComponents.Sidebar
{

    public class SidebarViewModel
    {
        public SidebarViewModel(SidebarType type, SidebarHeaderType headerType, string contentView, int activeIndex,
            string imgLocation, GoogleMapsViewModel googleMapsOptions = null, object formModel = null)
        {
            Type = type;
            ActiveIndex = activeIndex;
            ImgLocation = imgLocation;
            ContentView = contentView;
            HeaderType = headerType;
            GoogleMapsViewModel = googleMapsOptions;
            FormModel = formModel;


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
                case SidebarType.EmployerWithoutNavigation:
                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    Classes = "sidebar--employer";
                    break;
                case SidebarType.Parent:

                    if (imgLocation == null)
                    {
                        ImgLocation = "/campaign/images/worker-girl.jpg";
                    }
                    Classes = "sidebar--parent";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

        }

        public string ContentView { get; set; }

        public SidebarType Type { get; }
        public SidebarHeaderType HeaderType { get; }

        public int ActiveIndex { get; set; }
        public string ImgLocation { get; set; }
        public string Classes { get; set; }

        public GoogleMapsViewModel GoogleMapsViewModel { get; }
        public object FormModel { get; }
    }

}