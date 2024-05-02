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
            var cardWidthCssClass = control?.HubType == "Employers" ? "govuk-grid-column-one-third" : "govuk-grid-column-one-quarter";

            var card = new TagBuilder($"div");
            card.AddCssClass(cardWidthCssClass);
            card.InnerHtml.AppendHtml("<div class=\"fiu-card\">");
            card.InnerHtml.AppendHtml("<span class=\"fiu-card__category\">");
            card.InnerHtml.AppendHtml($"<a class=\"fiu-card__category-link\" href=\"{(control?.LandingPage?.Url).ToLower()}\">{control?.LandingPage?.Title}</a></span>");
            card.InnerHtml.AppendHtml($"<h3 class=\"fiu-card__heading\">{control?.Title}</h3>");
            card.InnerHtml.AppendHtml($"<p class=\"fiu-card__content\">{control?.Summary}</p>");
            card.InnerHtml.AppendHtml(
                $"<a href=\"{control?.Url.ToString().ToLower()}\" class=\"fiu-card__link\">{control?.Title}</a></div>");
            string result = card.WriteString();

            return new HtmlString(result);
        }

        public bool SupportsContent(IHtmlControl content)
        {
            return content is Card;
        }
    }
}
