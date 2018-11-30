using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.Modal;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeroHeadingType type, string caption, string classes)
        {
            return View("Default", new HeroHeadingViewModel(type, caption, classes));
        }
    }

    public class HeroHeadingViewModel
    {
        public HeroHeadingViewModel(HeroHeadingType type, string caption, string classes)
        {
            Type = type;

            setDefaults();

            if (!string.IsNullOrWhiteSpace(caption))
            {
                Caption = caption;
            }

            if (!string.IsNullOrWhiteSpace(classes))
            {
                Class = classes;
            }
        }

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