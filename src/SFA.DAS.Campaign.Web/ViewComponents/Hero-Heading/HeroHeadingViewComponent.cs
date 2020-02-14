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
                    break;
                case HeroHeadingType.Employer:
                    break;
                case HeroHeadingType.Parent:
                    break;
                case HeroHeadingType.FindApprenticeshipResults:
                    model.Type = HeroHeadingType.Apprentice;
                    view = "GoogleMaps";
                    break;
                case HeroHeadingType.FindApprenticeship:
                    model.Type = HeroHeadingType.Apprentice;
                    view = "FAA";
                    break;
                case HeroHeadingType.FindApprenticeshipTraining:
                    model.Type = HeroHeadingType.Employer;
                    view = "FAT";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(model.Type), model.Type, null);
            }


            return View(view, model);
        }
    }
}