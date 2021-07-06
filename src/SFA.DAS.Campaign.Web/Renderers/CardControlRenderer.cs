using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class CardControlRenderer : IControlRenderer
    {
        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Card;

            var card = new TagBuilder($"div");
            card.AddCssClass("govuk-grid-column-one-quarter");
            card.InnerHtml.AppendHtml("<div class=\"fiu-card\">");
            card.InnerHtml.AppendHtml("<span class=\"fiu-card__category\">");
            card.InnerHtml.AppendHtml($"<a class=\"fiu-card__category-link\" href=\"{control?.LandingPage?.Url}\">{control?.LandingPage?.Title}</a></span>");
            card.InnerHtml.AppendHtml($"<h3 class=\"fiu-card__heading\">{control?.Title}</h3>");
            card.InnerHtml.AppendHtml($"<p class=\"fiu-card__content\">{control?.Summary}</p>");
            card.InnerHtml.AppendHtml(
                $"<a href=\"{control?.Url}\" class=\"fiu-card__link\">Learn more <span class=\"fiu-vh\"> about {control?.Title}</span></a></div>");
            string result = card.WriteString();

            return new HtmlString(result);
        }

        public bool SupportsContent(IHtmlControl content)
        {
            return content is Card;
        }
    }
}
