using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeroHeadingType? type, string caption, string classes, IHtmlContent content)
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

            return View("Default", new HeroHeadingViewModel((HeroHeadingType)type, caption, classes, content));
        }
    }

   

}