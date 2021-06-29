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
    public class OrderedListControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is OrderedList;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as OrderedList;

            var ul = new TagBuilder("ol");

            foreach (var value in control.Items)
            {
                ul.InnerHtml.AppendHtml("<li>");

                foreach (var childValue in value)
                {
                    ul.InnerHtml.AppendHtml($"{childValue.CheckForFontEffects().CheckForAndConstructHyperlinks()}");
                }

                ul.InnerHtml.AppendHtml("</li>");
            }

            string result = ul.WriteString();

            return new HtmlString(result);
        }
    }
}
