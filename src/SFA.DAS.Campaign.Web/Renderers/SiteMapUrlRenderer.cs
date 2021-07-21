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
    public class SiteMapUrlRenderer : IControlRenderer
    {
        public bool SupportsContent(IHtmlControl content)
        {
            return content is SiteMapUrls;
        }

        public HtmlString Render(IHtmlControl content)
        {
            var control = content as SiteMapUrls;

            if (control == null)
            {
                return new HtmlString("");
            }

            var currentHub = string.Empty;

            var parentDiv = new TagBuilder("div");
            parentDiv.AddCssClass("govuk-grid-row");
            TagBuilder columnDiv = null;
            
            foreach (var url in control.Urls.Where(o => string.Compare(o.PageType, "hub", StringComparison.OrdinalIgnoreCase) == 0).OrderBy(o => o.Hub))
            {
                if (string.Compare(url.Hub, currentHub, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    if (columnDiv != null)
                    {
                        parentDiv.InnerHtml.AppendHtml(columnDiv.WriteString());
                    }

                    columnDiv = new TagBuilder("div");
                    columnDiv.AddCssClass("govuk-grid-column-one-third");

                    AddHubUrl(columnDiv, url);

                    currentHub = url.Hub;
                }

                AddLandingPageUrl(control.Urls.Where(o => string.Compare(o.PageType, "LandingPage", StringComparison.OrdinalIgnoreCase) == 0 &&
                                                          string.Compare(o.Hub, currentHub, StringComparison.OrdinalIgnoreCase) == 0).ToList(), columnDiv, control);
            }

            parentDiv.InnerHtml.AppendHtml(columnDiv.WriteString());

            string result = parentDiv.WriteString();

            return new HtmlString(result);
        }

        private void AddArticlePageUrls(IEnumerable<Url> articleUrls, TagBuilder parentDiv) 
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("govuk-list fiu-sitemap-list__child-list");

            foreach (var url in articleUrls)
            {
                ul.InnerHtml.AppendHtml(
                    $"<li><a href=\"{url.Hub}/{url.Slug}\" class=\"fiu-link fiu-link--{GetCssClassNameForHub(url.Hub)}\">{url.Title}</a></li>");
            }

            parentDiv.InnerHtml.AppendHtml(ul.WriteString());
        }

        private void AddLandingPageUrl(List<Url> landingPageUrls, TagBuilder parentDiv, SiteMapUrls sitemapUrls)
        {
            string currentLandingPage = null;
            var ul = new TagBuilder("ul"); 
            ul.AddCssClass("govuk-list fiu-sitemap-list");

            foreach (var landingPageUrl in landingPageUrls)
            {
                if (string.Compare(currentLandingPage, landingPageUrl.Slug, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    ul.InnerHtml.AppendHtml(
                        $"<li><a href=\"/{landingPageUrl.Hub}/{landingPageUrl.Slug}\" class=\"fiu-link fiu-link--{ GetCssClassNameForHub(landingPageUrl.Hub)} fiu-sitemap-list__link\">{landingPageUrl.Title}</a>");

                    currentLandingPage = landingPageUrl.Slug;

                    var page = currentLandingPage;

                    AddArticlePageUrls(sitemapUrls.Urls.Where(o => 
                    string.Compare(o.ParentSlug, page, StringComparison.OrdinalIgnoreCase) == 0 &&
                                                                   string.Compare(o.PageType, "Article", StringComparison.OrdinalIgnoreCase) == 0), ul);

                    ul.InnerHtml.AppendHtml("</li>");
                }
            }

            parentDiv.InnerHtml.AppendHtml(ul.WriteString());
        }

        private void AddHubUrl(TagBuilder columnDiv, Url control)
        {
            columnDiv.InnerHtml.AppendHtml(
                $"<div class=\"fiu-feature-text\"><a class=\"fiu-tag fiu-tag--{ GetCssClassNameForHub(control.Hub)} fiu-panel__tag\" asp-controller=\"Hub\" asp-action=\"{control.Hub}\">{control.Hub}</a></div>");
        }

        private string GetCssClassNameForHub(string hub)
        {
            if (string.Compare(hub, "apprentices", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "citizens";
            }

            return hub.ToLowerInvariant();
        }
    }
}
