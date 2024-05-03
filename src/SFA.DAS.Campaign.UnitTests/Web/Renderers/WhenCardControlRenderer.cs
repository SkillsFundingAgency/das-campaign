using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenCardControlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_RelatedArticle_Then_Supports_Content_Returns_True(Card card, CardControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(card);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Attachment_Then_Supports_Content_Returns_False(Table table, CardControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Attachment_Then_Render_Returns_The_Html(CardControlRenderer renderer)
        {
            var attachment = new Card()
            {
                Description = "description",
                Title = "title",
                HubType = "hub",
                Slug = "slug",
                Summary = "summary",
                LandingPage = new CardLandingPage
                {
                    Hub = "lhub",
                    Slug = "lslug",
                    Title = "ltitle"
                }
            };
          
            var actual = renderer.Render(attachment);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<div class=\"govuk-grid-column-one-quarter\"><div class=\"fiu-card\"><span class=\"fiu-card__category\"><a class=\"fiu-card__category-link\" href=\"/lhub/lslug\">ltitle</a></span><h3 class=\"fiu-card__heading\">title</h3><p class=\"fiu-card__content\">summary</p><a href=\"/hub/slug\" class=\"fiu-card__link\">title</a></div></div>");
        }
    }
}
