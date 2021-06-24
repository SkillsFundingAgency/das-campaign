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
    public class UnorderedListControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is UnorderedList;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as UnorderedList;

            var ul = new TagBuilder("ul");

            foreach (var value in control.Items)
            {
                ul.InnerHtml.AppendHtml($"<li>{value.CheckForFontEffects().CheckForAndConstructHyperlinks()}</li>");
            }

            string result = ul.WriteString();

            return new HtmlString(result);
        }
    }
}
