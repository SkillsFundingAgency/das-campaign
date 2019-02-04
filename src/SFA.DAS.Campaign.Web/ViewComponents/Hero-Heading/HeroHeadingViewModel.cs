using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
  

    public class HeroHeadingViewModel
    {
        public HeroHeadingViewModel(HeroHeadingType type, string caption, string classes, IHtmlContent content, GoogleMapsViewModel googleMapsViewModel)
        {
            Type = type;
            Content = content;
            setDefaults();
            GoogleMapsViewModel = googleMapsViewModel;

            if (!string.IsNullOrWhiteSpace(caption))
            {
                Caption = caption;
            }

            if (!string.IsNullOrWhiteSpace(classes))
            {
                Class = Class + " " + classes;
            }
        }

        public IHtmlContent Content { get; internal set; }

        public HeroHeadingType Type { get; }

        public string Caption { get; internal set; }

        public string Class { get; internal set; }
        public GoogleMapsViewModel GoogleMapsViewModel { get; }
        private string setDefaults()
        {
            var caption = "";

            switch (Type)
            {
                case HeroHeadingType.Apprentice:
                    Caption = "Apprentice";
                    Class = "hero-heading__caption--apprentice";
                    break;
                case HeroHeadingType.Employer:
                    Caption = "Employer";
                    Class = "hero-heading__caption--employer";
                    break;
            }

            return caption;
        }
    }

}