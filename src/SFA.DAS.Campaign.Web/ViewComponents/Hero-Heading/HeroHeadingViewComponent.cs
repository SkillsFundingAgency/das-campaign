using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(HeroHeadingType? type, string caption, string classes, IHtmlContent content, GoogleMapsViewModel googleMapsOptions)
        {
            if (type == null)
            {
                switch (ViewContext.RouteData.Values["Controller"])
                {
                    case "Apprentice":
                        type = HeroHeadingType.Apprentice;
                        break;
                    case "Employer":
                        type = HeroHeadingType.Employer;
                        break;
                    case "Parents":
                        type = HeroHeadingType.Parent;
                        break;
                    default:
                        type = HeroHeadingType.None;
                        break;
                }
            }

            var view = "Default";

            switch (type)
            {
                case HeroHeadingType.None:
                    break;
                case HeroHeadingType.Apprentice:
                    break;
                case HeroHeadingType.Employer:
                    break;
                        case HeroHeadingType.Parent:
                    break;
                case HeroHeadingType.FindApprenticeshipResults:
                    type = HeroHeadingType.Apprentice;
                    view = "GoogleMaps";
                    break;
                case HeroHeadingType.FindApprenticeship:
                    type = HeroHeadingType.Apprentice;
                    view = "FAA";
                    break;
                case HeroHeadingType.FindApprenticeshipTraining:
                    type = HeroHeadingType.Employer;
                    view = "FAT";
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            if (type == HeroHeadingType.FindApprenticeshipResults)
            {
               
            }

            return View(view, new HeroHeadingViewModel((HeroHeadingType)type, caption, classes, content, googleMapsOptions));
        }
    }
}