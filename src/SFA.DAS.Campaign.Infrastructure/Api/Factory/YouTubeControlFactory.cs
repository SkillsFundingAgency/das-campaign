using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class YouTubeControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var yt = new YouTube() { Url = control.Values.FirstOrDefault() };

            return yt;
        }

        public bool IsValid(Item control)
        {
            if (string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0 && control.Values.Any() && control.Values.FirstOrDefault().StartsWith("https://www.youtube.com"))
            {
                return true;
            }

            return false;
        }
    }
}
