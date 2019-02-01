﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

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
        public SidebarHeaderType HeaderType { get; }

        public int ActiveIndex { get; set; }
        public string ImgLocation { get; set; }
        public string Classes { get; set; }

        public GoogleMapsViewModel GoogleMapsViewModel { get; }
        public object FormModel { get; }
    }

}