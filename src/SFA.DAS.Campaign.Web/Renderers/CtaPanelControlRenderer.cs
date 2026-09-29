using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Renderers
{
    // Produces the same markup as the Hubs/_HubCtaPanel partial
    public class CtaPanelControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is CtaPanel;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as CtaPanel;

            var panel = new TagBuilder("a");
            panel.AddCssClass("fiu-panel fiu-panel--left-align fiu-cta-panel");
            panel.Attributes.Add("href", control.Url ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(control.Icon))
            {
                panel.InnerHtml.AppendHtml(BuildIcon(control.Icon));
            }
            
            if (!string.IsNullOrWhiteSpace(control.Heading))
            {
                var heading = new TagBuilder("h2");
                heading.AddCssClass("govuk-heading-m govuk-!-margin--top-0");
                heading.InnerHtml.Append(control.Heading);
                panel.InnerHtml.AppendHtml(heading);
            }

            if (!string.IsNullOrWhiteSpace(control.Description))
            {
                var description = new TagBuilder("p");
                description.InnerHtml.Append(control.Description);
                panel.InnerHtml.AppendHtml(description);
            }

            if (!string.IsNullOrWhiteSpace(control.ButtonText))
            {
                panel.InnerHtml.AppendHtml(BuildAction(control.ButtonText));
            }

            return new HtmlString(panel.WriteString());
        }

        private static TagBuilder BuildIcon(string iconName)
        {
            var use = new TagBuilder("use");
            use.Attributes.Add("href", $"/images/sprite.svg#{iconName.Trim().ToLowerInvariant()}");

            var svg = new TagBuilder("svg");
            svg.AddCssClass("fiu-icon");
            svg.Attributes.Add("aria-hidden", "true");
            svg.Attributes.Add("focusable", "false");
            svg.InnerHtml.AppendHtml(use);

            var icon = new TagBuilder("span");
            icon.AddCssClass("fiu-cta-panel__icon");
            icon.Attributes.Add("aria-hidden", "true");
            icon.InnerHtml.AppendHtml(svg);

            return icon;
        }
        

        private static TagBuilder BuildAction(string buttonText)
        {
            var button = new TagBuilder("span");
            button.AddCssClass("fiu-cta-panel__button");
            button.InnerHtml.Append(buttonText);

            return button;
        }
    }
}
