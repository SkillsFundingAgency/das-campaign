using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.Web.Renderers
{
    public class TabbedContentRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is Tabs;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Tabs;

            if (control == null)
            {
                return new HtmlString("");
            }

            var parentDiv = new TagBuilder("div");
            parentDiv.AddCssClass("fiu-tabs");
            parentDiv.Attributes.Add("data-fiu-tabs", bool.TrueString);

            //construct title of tabs
            ConstructTabHeaders(parentDiv, control);
            ConstructTabContent(parentDiv, control);
            return new HtmlString(parentDiv.WriteString());
        }

        private void ConstructTabContent(TagBuilder parentDiv, Tabs tab)
        {
            var addHiddenCssClass = false;

            foreach (var tabContent in tab.TabbedContents)
            {
                var div = new TagBuilder("div");
                div.AddCssClass("fiu-tabs__panel");
                if (addHiddenCssClass)
                {
                    div.AddCssClass("fiu-tabs__panel--hidden");
                }
                else
                {
                    addHiddenCssClass = true;
                }

                div.MergeAttribute("id", tabContent.TabTitle.EnsureHrefUsableTitle());

                var articleEl = new TagBuilder("article");
                articleEl.AddCssClass("fiu-article");
                articleEl.InnerHtml.AppendHtml(tabContent.Content.ToHtml());
                div.InnerHtml.AppendHtml(articleEl.WriteString());

                parentDiv.InnerHtml.AppendHtml(div.WriteString());
            }
        }

        private void ConstructTabHeaders(TagBuilder parentDiv, Tabs tab)
        {
            var h2 = new TagBuilder("h2");
            h2.AddCssClass("fiu-tabs__title");
            h2.InnerHtml.Append("Contents");
            parentDiv.InnerHtml.AppendHtml(h2.WriteString());

            var addSelectedCssClass = true;
            var ul = new TagBuilder("ul");
            ul.AddCssClass("fiu-tabs__list");

            foreach (var tabbedContent in tab.TabbedContents)
            {
                var li = new TagBuilder("li");
                var href = new TagBuilder("a");

                if (addSelectedCssClass)
                {
                    li.AddCssClass("fiu-tabs__list-item fiu-tabs__list-item--selected");
                    addSelectedCssClass = false;
                }
                else
                {
                    li.AddCssClass("fiu-tabs__list-item");
                }
                
                href.AddCssClass("fiu-tabs__tab");
                href.MergeAttribute("href", $"#{tabbedContent.TabTitle.EnsureHrefUsableTitle()}");
                href.InnerHtml.Append(tabbedContent.TabTitle);
                li.InnerHtml.AppendHtml(href.WriteString());
                ul.InnerHtml.AppendHtml(li.WriteString());
            }

            parentDiv.InnerHtml.AppendHtml(ul.WriteString());
        }
    }
}
