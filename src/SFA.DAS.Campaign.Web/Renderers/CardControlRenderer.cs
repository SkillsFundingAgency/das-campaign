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
            card.AddCssClass("fiu-card");

            card.InnerHtml.AppendHtml($"<h3 class=\"fiu-card__heading\">{control?.Title}</h3>");
            card.InnerHtml.AppendHtml($"<p class=\"fiu-card__content\">{control?.Summary}</p>");
            card.InnerHtml.AppendHtml(
                $"<a href=\"{control?.Url.ToString().ToLower()}\" class=\"fiu-card__link\">{control?.Title}</a>");
            string result = card.WriteString();

            return new HtmlString(result);
        }

        public bool SupportsContent(IHtmlControl content)
        {
            return content is Card;
        }
    }
}
