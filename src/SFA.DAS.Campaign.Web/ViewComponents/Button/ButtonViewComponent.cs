using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Web.Models;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.ViewComponents.Button
{
    public class ButtonViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ButtonType type, ButtonStyle style,  string name, string text, string classes = null, bool shadow = true, bool sparks = false, string href = "#")
        {
            var button = new ButtonViewModel(style)
            {
                Type = type,
                Text = text,
                Class = classes,
                Shadow = shadow,
                Sparks = sparks,
                Href = href,
                Name = name
            };

            switch (type)
            {
                case ButtonType.Button:
                    return View("Button", button);
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
