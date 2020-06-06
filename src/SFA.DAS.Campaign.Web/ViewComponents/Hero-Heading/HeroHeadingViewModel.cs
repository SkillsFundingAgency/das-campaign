using System;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Web.Models;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.ViewComponents
{


    public class HeroHeadingViewModel
    {
        private HeroHeadingType _type;
        private string _controller;
        private string _class;
        private string _sectionTitle;

        public IHtmlContent Content { get; set; }

        public HeroHeadingType Type
        {
            get
            {
                if (_type == null)
                {
                    switch (_controller)
                    {
                        case "Apprentice":
                            return HeroHeadingType.Apprentice;
                            break;
                        case "Employer":
                            return HeroHeadingType.Employer;
                            break;
                        case "Parents":
                            return HeroHeadingType.Parent;
                            break;
                        default:
                            return HeroHeadingType.None;
                            break;
                    }
                }
                else
                {
                    return _type;
                }
            }

            set => _type = value;
        }

        public string SectionTitle
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_sectionTitle))
                {
                    switch (_type)
                    {
                        case HeroHeadingType.Apprentice:
                            return "Apprentice";
                        case HeroHeadingType.Employer:
                            return "Employer";
                        case HeroHeadingType.Parent:
                            return "Parents";
                    }
                    return String.Empty;
                }

                return _sectionTitle;

            }
            set => _sectionTitle = value;
        }

        public string Class
        {
            get
            {
                switch (_type)
                {
                    case HeroHeadingType.Apprentice:
                        return String.Join(' ',
                            new string[] { "hero-heading__caption--apprentice", _class }.Where(s =>
                                  !string.IsNullOrEmpty(s)));
                    case HeroHeadingType.Employer:
                        return String.Join(' ',
                            new string[] { "hero-heading__caption--employer", _class }.Where(s =>
                                !string.IsNullOrEmpty(s)));
                    case HeroHeadingType.Parent:
                        return String.Join(' ',
                            new string[] { "hero-heading__caption--parent", _class }.Where(s =>
                                !string.IsNullOrEmpty(s)));
                }

                return _class;
            }
            set { _class = value; }
        }

        public GoogleMapsViewModel GoogleMapsViewModel { get; set; }
        public FindApprenticeshipSearchModel FaaSearchResultsViewModel { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string Controller { internal get; set; }

        public string SectionUrl { get; set; }
        public string SubSectionTitle { get; set; }
        public string SubSectionUrl { get; set; }
    }
}