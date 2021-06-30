using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class HeadingControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is Heading;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Heading;

            var heading = new TagBuilder($"h{control.HeadingSize}");

            foreach (var value in control.Content)
            {
                heading.InnerHtml.AppendHtml(value.CheckForFontEffects().CheckForAndConstructHyperlinks());
            }

            string result = heading.WriteString();

            return new HtmlString(result);
        }
    }
}
