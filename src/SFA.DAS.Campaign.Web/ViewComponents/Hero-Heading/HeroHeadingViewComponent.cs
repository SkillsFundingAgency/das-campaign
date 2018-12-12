using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeroHeadingType type, string caption, string classes, IHtmlContent content)
        {
           
            return View("Default", new HeroHeadingViewModel(type, caption, classes, content));
        }
    }

   

}