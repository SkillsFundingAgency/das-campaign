using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeroHeadingType? type, string caption, string classes, IHtmlContent content, GoogleMapsViewModel googleMapsOptions)
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
                    default:
                        type = HeroHeadingType.None;
                        break;
                }
            }

            var view = "Default";
            if (type == HeroHeadingType.FindApprenticeship)
            {
                type = HeroHeadingType.Apprentice;
                view = "GoogleMaps";
            }

            return View(view, new HeroHeadingViewModel((HeroHeadingType)type, caption, classes, content, googleMapsOptions));
        }
    }

   

}