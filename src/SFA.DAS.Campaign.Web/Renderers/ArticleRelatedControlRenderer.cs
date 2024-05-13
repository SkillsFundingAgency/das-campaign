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
    public class ArticleRelatedControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is ArticleRelated;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as ArticleRelated;

            var quote = new TagBuilder($"div");
            quote.InnerHtml.AppendHtml("<div class=\"fiu-next-link-wrap\">");
            quote.InnerHtml.AppendHtml(
                $"<a href=\"{control.Url}\" class=\"fiu-button fiu-button--employers fiu-next-link\">Next: {control.Title} &gt;</a></div>");
            string result = quote.WriteString();

            return new HtmlString(result);
        }
    }
}
