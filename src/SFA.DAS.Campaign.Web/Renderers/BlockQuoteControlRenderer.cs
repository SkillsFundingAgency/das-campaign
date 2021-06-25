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
    public class BlockQuoteControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is BlockQuote;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as BlockQuote;

            var quote = new TagBuilder($"blockquote");

            foreach (var value in control.Content)
            {
                quote.InnerHtml.AppendHtml(value.CheckForFontEffects().CheckForAndConstructHyperlinks());
            }

            string result = quote.WriteString();

            return new HtmlString(result);
        }
    }
}
