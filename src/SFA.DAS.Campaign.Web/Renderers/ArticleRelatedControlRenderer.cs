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
            quote.AddCssClass("govuk-grid-column-one-half");
            quote.InnerHtml.AppendHtml("<div class=\"fiu-card\">");
            quote.InnerHtml.AppendHtml("<span class=\"fiu-card__category\">");
            quote.InnerHtml.AppendHtml($"<a class=\"fiu-card__category-link\" href=\"{control.Url}\">{control.Title}</a></span>");
            quote.InnerHtml.AppendHtml($"<h3 class=\"fiu-card__heading\">{control.Title}</h3>");
            quote.InnerHtml.AppendHtml($"<p class=\"fiu-card__content\">{control.Description}</p>");
            quote.InnerHtml.AppendHtml(
                $"<a href=\"{control.Url}\" class=\"fiu-card__link\">Learn more <span class=\"fiu-vh\"> about {control.Title}</span></a></div>");
            string result = quote.WriteString();

            return new HtmlString(result);
        }
    }
}
