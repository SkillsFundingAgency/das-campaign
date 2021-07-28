using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenSiteMapUrlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_SiteMapUrls_Then_Supports_Content_Returns_True(SiteMapUrls urls, SiteMapUrlRenderer renderer)
        {
            var actual = renderer.SupportsContent(urls);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_SiteMapUrls_Then_Supports_Content_Returns_False(Table table, SiteMapUrlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_SiteMapUrls_Then_Render_Returns_The_Html(SiteMapUrlRenderer renderer)
        {
            var urls = new SiteMapUrls();

            urls.Urls.Add(new Url
            {
                PageType = "hub",
                Title = "hub",
                Hub = "hub",
                ParentSlug = "",
                Slug = "hub"
            });

            urls.Urls.Add(new Url
            {
                PageType = "LandingPage",
                Title = "LandingPage",
                Hub = "hub",
                ParentSlug = "",
                Slug = "LandingPage"
            });

            urls.Urls.Add(new Url
            {
                PageType = "article",
                Title = "article",
                Hub = "hub",
                ParentSlug = "LandingPage",
                Slug = "article"
            });


            var actual = renderer.Render(urls);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"govuk-grid-row\"><div class=\"govuk-grid-column-one-third\"><div class=\"fiu-feature-text\"><a class=\"fiu-tag fiu-tag--hub fiu-panel__tag\" href=\"/hub\">hub</a></div><ul class=\"govuk-list fiu-sitemap-list\"><li><a href=\"/hub/LandingPage\" class=\"fiu-link fiu-link--hub fiu-sitemap-list__link\">LandingPage</a><ul class=\"govuk-list fiu-sitemap-list__child-list\"><li><a href=\"/hub/article\" class=\"fiu-link fiu-link--hub\">article</a></li></ul></li></ul></div></div>");
        }
    }
}
