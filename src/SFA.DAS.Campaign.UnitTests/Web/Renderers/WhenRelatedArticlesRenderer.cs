using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenRelatedArticlesRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_RelatedArticle_Then_Supports_Content_Returns_True(ArticleRelated article, ArticleRelatedControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(article);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Attachment_Then_Supports_Content_Returns_False(Table table, ArticleRelatedControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Attachment_Then_Render_Returns_The_Html(ArticleRelatedControlRenderer renderer)
        {
            var attachment = new ArticleRelated()
            {
                Description = "description",
                Title = "title",
                HubType = "hub",
                Slug = "slug",
                Summary = "summary"
            };
          
            var actual = renderer.Render(attachment);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"govuk-grid-column-one-half\"><div class=\"fiu-card\"><span class=\"fiu-card__category\"><a class=\"fiu-card__category-link\" href=\"/hub/slug\">title</a></span><h3 class=\"fiu-card__heading\">title</h3><p class=\"fiu-card__content\">description</p><a href=\"/hub/slug\" class=\"fiu-card__link\">Learn more <span class=\"fiu-vh\"> about title</span></a></div></div>");
        }
    }
}
