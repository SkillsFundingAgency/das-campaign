using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;

namespace SFA.DAS.Campaign.Web.Views.Shared.Components.Button
{
    public class ButtonViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ButtonType type,ButtonStyle style, string text, string classes = null, bool shadow = true, bool sparks = false, string href="#")
        {
            var button =new ButtonViewModel(style)
            {
                Text = text,
                Class = classes,
                Shadow = shadow,
                Sparks = sparks,
                Href = href
            };

            switch (type)
            {
                case ButtonType.Button:
                    return View("Button",button);
                    break;
                case ButtonType.Input:
                    return View("Input", button);
                    break;
                case ButtonType.Anchor:
                    return View("Anchor", button);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
           
            
        }
    }
}
