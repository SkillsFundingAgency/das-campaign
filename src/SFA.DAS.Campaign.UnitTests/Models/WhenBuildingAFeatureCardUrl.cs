using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.UnitTests.Models
{
    public class WhenBuildingAFeatureCardUrl
    {
        [TestCase("Apprentices", "is-it-right", "Article", "/apprentices/is-it-right")]
        [TestCase("Employers", "announcements", "Article", "/employers/announcements")]
        [TestCase("Employers", "get-started", "LandingPage", "/employers/get-started")]
        [TestCase("Employers", "employers", "Hub", "/employers")]
        [TestCase("Apprentices", null, "Article", "/apprentices")]
        public void Then_An_Internal_Page_Url_Uses_The_Lowercase_Hub_And_Slug(string hub, string slug, string pageType, string expected)
        {
            var card = new FeatureCard
            {
                LinkType = "Internal",
                InternalPage = new FeatureCardInternalPage { Hub = hub, Slug = slug, PageType = pageType }
            };

            card.Url.Should().Be(expected);
        }

        [Test]
        public void Then_An_Internal_Page_Without_A_Hub_Has_No_Url()
        {
            var card = new FeatureCard
            {
                LinkType = "Internal",
                InternalPage = new FeatureCardInternalPage { Slug = "some-slug" }
            };

            card.Url.Should().BeNull();
        }

        [Test]
        public void Then_An_Internal_Card_Without_A_Page_Has_No_Url()
        {
            var card = new FeatureCard { LinkType = "Internal", ExternalUrl = "https://example.com" };

            card.Url.Should().BeNull();
        }

        [TestCase("External")]
        [TestCase("external")]
        public void Then_An_External_Card_Uses_The_External_Url(string linkType)
        {
            var card = new FeatureCard
            {
                LinkType = linkType,
                ExternalUrl = "https://www.findapprenticeship.service.gov.uk/",
                InternalPage = new FeatureCardInternalPage { Hub = "Apprentices", Slug = "ignored" }
            };

            card.IsExternal.Should().BeTrue();
            card.Url.Should().Be("https://www.findapprenticeship.service.gov.uk/");
        }

        [TestCase("Employers", true)]
        [TestCase("employers", true)]
        [TestCase("Apprentices", false)]
        [TestCase(null, false)]
        public void Then_The_Audience_Determines_Whether_The_Card_Is_For_Employers(string audience, bool expected)
        {
            new FeatureCard { Audience = audience }.IsForEmployers.Should().Be(expected);
        }
    }
}
