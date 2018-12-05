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

    public class HeroHeadingViewModel
    {
        public HeroHeadingViewModel(HeroHeadingType type, string caption, string classes, IHtmlContent content)
        {
            Type = type;
            Content = content;
            setDefaults();

            if (!string.IsNullOrWhiteSpace(caption))
            {
                Caption = caption;
            }

            if (!string.IsNullOrWhiteSpace(classes))
            {
                Class = Class + " " + classes;
            }
        }

        public IHtmlContent Content { get; set; }

        public HeroHeadingType Type { get; }

        public string Caption {get; set; }

        public string Class { get; set; }

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