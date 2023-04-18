using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public HttpContext HttpContext { get; set; }
        public ITempDataDictionary TempDataDictionary { get; set; }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as Tabs;

            if (control == null)
            {
                return new HtmlString("");
            }

            var parentDiv = new TagBuilder("div");
            parentDiv.AddCssClass("govuk-grid-row app-anchor-links");

            var leftCol = new TagBuilder("div");
            leftCol.AddCssClass("govuk-grid-column-one-third");

            var rightCol = new TagBuilder("div");
            rightCol.AddCssClass("govuk-grid-column-two-thirds");

            //construct title of tabs
            ConstructTabHeaders(leftCol, control);
            ConstructTabContent(rightCol, control);

            parentDiv.InnerHtml.AppendHtml(leftCol.WriteString());
            parentDiv.InnerHtml.AppendHtml(rightCol.WriteString());

            return new HtmlString(parentDiv.WriteString());
        }

        private void ConstructTabContent(TagBuilder parentDiv, Tabs tab)
        {
            foreach (var tabContent in tab.TabbedContents)
            {
                var div = new TagBuilder("div");
                div.AddCssClass("app-anchor-links__panel");
                div.MergeAttribute("id", tabContent.TabTitle.EnsureHrefUsableTitle());

                var articleEl = new TagBuilder("article");
                articleEl.AddCssClass("fiu-article");
                articleEl.InnerHtml.AppendHtml(tabContent.Content.ToHtml());
                if (tabContent.FindTraineeShip)
                {
                    var viewText =
                        tabContent.RenderViewAsync("~/Views/Shared/Panels/_FindTraineeship.cshtml", HttpContext, TempDataDictionary).Result;

                    articleEl.InnerHtml.AppendHtml(viewText);
                }

                div.InnerHtml.AppendHtml(articleEl.WriteString());
                
                parentDiv.InnerHtml.AppendHtml(div.WriteString());
            }
        }

        private void ConstructTabHeaders(TagBuilder parentDiv, Tabs tab)
        {
            var h2 = new TagBuilder("h2");
            h2.AddCssClass("govuk-heading-m");
            h2.InnerHtml.Append("On this page");
            parentDiv.InnerHtml.AppendHtml(h2.WriteString());
            var ul = new TagBuilder("ul");
            ul.AddCssClass("govuk-list govuk-list--spaced app-anchor-links__list");

            foreach (var tabbedContent in tab.TabbedContents)
            {
                var li = new TagBuilder("li");
                var href = new TagBuilder("a");
                li.AddCssClass("app-anchor-links__list-item");
                
                href.AddCssClass("govuk-link govuk-link--no-visited-state app-anchor-links__link");
                href.MergeAttribute("href", $"#{tabbedContent.TabTitle.EnsureHrefUsableTitle()}");
                href.InnerHtml.Append(tabbedContent.TabTitle);
                li.InnerHtml.AppendHtml(href.WriteString());
                ul.InnerHtml.AppendHtml(li.WriteString());
            }

            parentDiv.InnerHtml.AppendHtml(ul.WriteString());
        }
    }
}
