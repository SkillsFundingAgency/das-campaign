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

            var card = new TagBuilder("a");
            card.AddCssClass("fiu-card");
            card.Attributes.Add("href", control?.Url.ToLower());

            if (control?.CardImage?.Url != null)
            {
                card.InnerHtml.AppendHtml($"<span class=\"fiu-card__img\" style=\"background-image:url({control.CardImage.Url})\"></span>");
            }

            card.InnerHtml.AppendHtml($"<div class=\"fiu-card__body\"><h3 class=\"fiu-card__heading\">{control?.Title}</h3>");
            card.InnerHtml.AppendHtml($"<p class=\"fiu-card__content\">{control?.Summary}</p></div>");
            string result = card.WriteString();

            return new HtmlString(result);
        }

        public bool SupportsContent(IHtmlControl content)
        {
            return content is Card;
        }
    }
}
