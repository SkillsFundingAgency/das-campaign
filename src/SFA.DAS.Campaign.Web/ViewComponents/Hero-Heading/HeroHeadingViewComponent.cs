using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.ViewComponents.GoogleMaps;
using SFA.DAS.Campaign.Web.ViewComponents.HeroHeading;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class HeroHeadingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(HeroHeadingViewModel model)
        {

            if (model == null)
            {
                model = new HeroHeadingViewModel();
            }
            
            var view = "Default";

            switch (model.Type)
            {
                case HeroHeadingType.None:
                    break;
                case HeroHeadingType.Apprentice:
                    model.SectionUrl = Url.Action("Apprentice", "RealStories");
                    break;
                case HeroHeadingType.Employer:
                    model.SectionUrl = Url.Action("Employer", "RealStories");
                    break;
                case HeroHeadingType.Parent:
                    model.SectionUrl = Url.Action("TheirCareer", "Parents");
                    break;
                case HeroHeadingType.FindApprenticeshipResults:
                    model.Type = HeroHeadingType.Apprentice;
                    view = "GoogleMaps";
                    break;
                case HeroHeadingType.FindApprenticeship:
                    model.Type = HeroHeadingType.Apprentice;
                    model.SectionUrl = Url.Action("Apprentice", "RealStories");
                    view = "FAA";
                    break;
                case HeroHeadingType.FindApprenticeshipTraining:
                    model.Type = HeroHeadingType.Employer;
                    model.SectionUrl = Url.Action("Employer", "RealStories");
                    view = "FAT";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(model.Type), model.Type, null);
            }


            return View(view, model);
        }
    }
}