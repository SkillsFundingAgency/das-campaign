using System;
using System.Collections.Generic;
using System.Text;
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
        public void Is_Passed_An_Object_Of_SiteMapUrls_Then_Render_Returns_The_Html(SiteMapUrls urls, SiteMapUrlRenderer renderer)
        {
            var actual = renderer.Render(urls);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"fiu-attachment\"><h2 class=\"govuk-heading-m fiu-attachment__heading\">title</h2><dl class=\"fiu-attachment__meta\"><dt class=\"fiu-attachment__meta-title\">File type</dt><dd class=\"fiu-attachment__meta-description\">pdf</dd><dt class=\"fiu-attachment__meta-title\">File size</dt><dd class=\"fiu-attachment__meta-description\">1KB</dd></dl><p class=\"govuk-body fiu-attachment__description\">description</p><p class=\"govuk-body fiu-attachment__link-wrap\"><a href=\"http://image.url\" class=\"govuk-link fiu-attachment__link\" target=\"_blank\">Download <span class=\"fiu-vh\">title</span></a></p><span class=\"fiu-attachment__icon\"><span class=\"fiu-attachment__icon-label\">pdf</span></span></div>");
        }
    }
}
