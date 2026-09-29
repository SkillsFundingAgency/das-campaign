using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Web.Renderers
{
    // Produces the same markup as the Hubs/_HubStatistics partial
    public class StatisticsControlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is HubStatistic;
        }

        public HtmlString Render(IHtmlControl content)
        {
            return Render(new List<HubStatistic> { content as HubStatistic });
        }

        public HtmlString Render(IEnumerable<HubStatistic> statistics)
        {
            var list = new TagBuilder("ul");
            list.AddCssClass("fiu-statistics");
            list.Attributes.Add("role", "list");

            foreach (var statistic in statistics)
            {
                var item = new TagBuilder("li");
                item.AddCssClass("fiu-statistics__item");
                item.Attributes.Add("role", "listitem");
                item.InnerHtml.AppendHtml(statistic.IsStat ? BuildStatistic(statistic) : BuildQuote(statistic));

                list.InnerHtml.AppendHtml(item);
            }

            return new HtmlString(list.WriteString());
        }

        private static TagBuilder BuildStatistic(HubStatistic statistic)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("fiu-statistic");

            container.InnerHtml.AppendHtml(BuildText("p", "fiu-statistic__highlight", statistic.HighlightValue));

            if (!string.IsNullOrWhiteSpace(statistic.Text))
            {
                container.InnerHtml.AppendHtml(BuildText("p", "fiu-statistic__text", statistic.Text));
            }

            AddReference(container, statistic);

            return container;
        }

        private static TagBuilder BuildQuote(HubStatistic statistic)
        {
            var figure = new TagBuilder("figure");
            figure.AddCssClass("fiu-quote fiu-statistic-quote");

            if (!string.IsNullOrWhiteSpace(statistic.Text))
            {
                figure.InnerHtml.AppendHtml(BuildText("blockquote", "fiu-quote__blockquote", statistic.Text));
            }

            if (!string.IsNullOrWhiteSpace(statistic.QuoteName) || !string.IsNullOrWhiteSpace(statistic.QuoteRole))
            {
                var caption = new TagBuilder("figcaption");
                caption.AddCssClass("fiu-quote__caption");
                caption.InnerHtml.Append(statistic.QuoteName ?? string.Empty);

                if (!string.IsNullOrWhiteSpace(statistic.QuoteRole))
                {
                    caption.InnerHtml.AppendHtml(BuildText("span", "fiu-quote__caption-description", statistic.QuoteRole));
                }

                figure.InnerHtml.AppendHtml(caption);
            }

            AddReference(figure, statistic);

            return figure;
        }

        private static void AddReference(TagBuilder container, HubStatistic statistic)
        {
            if (!string.IsNullOrWhiteSpace(statistic.ReferenceText))
            {
                container.InnerHtml.AppendHtml(BuildText("p", "fiu-statistic__reference", statistic.ReferenceText));
            }
        }

        private static TagBuilder BuildText(string tagName, string cssClass, string text)
        {
            var tag = new TagBuilder(tagName);
            tag.AddCssClass(cssClass);
            tag.InnerHtml.Append(text);

            return tag;
        }
    }
}
