using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenDeserialisingTheHomepageJson
    {
        private const string Json = @"{
  ""homepage"": {
    ""pageTitle"": ""Your guide to apprenticeships"",
    ""browserTitle"": ""Apprenticeships"",
    ""introText"": ""Intro text"",
    ""featureCards"": [
      {
        ""id"": ""1H202vCRDsn2RJjv7HMqkj"",
        ""heading"": ""Is an apprenticeship right for you?"",
        ""description"": ""Not sure about what to do?"",
        ""audience"": ""Apprentices"",
        ""linkType"": ""Internal"",
        ""externalUrl"": null,
        ""internalPage"": {
          ""slug"": ""is-an-apprenticeship-right-for-you"",
          ""title"": ""Check if an apprenticeship is right for you"",
          ""hub"": ""Apprentices"",
          ""pageType"": ""Article"",
          ""parentSlug"": ""are-they-right-for-you""
        }
      },
      {
        ""id"": ""external-1"",
        ""heading"": ""Find an apprenticeship"",
        ""description"": ""Search and apply."",
        ""audience"": ""Employers"",
        ""linkType"": ""External"",
        ""externalUrl"": ""https://www.findapprenticeship.service.gov.uk/"",
        ""internalPage"": null
      }
    ]
  }
}";

        [Test]
        public void Then_The_Homepage_Fields_Are_Mapped()
        {
            var actual = JsonConvert.DeserializeObject<GetHomepageResponse>(Json);

            actual.Homepage.Should().NotBeNull();
            actual.Homepage.PageTitle.Should().Be("Your guide to apprenticeships");
            actual.Homepage.BrowserTitle.Should().Be("Apprenticeships");
            actual.Homepage.IntroText.Should().Be("Intro text");
        }

        [Test]
        public void Then_An_Internal_Feature_Card_Is_Mapped()
        {
            var actual = JsonConvert.DeserializeObject<GetHomepageResponse>(Json);

            actual.Homepage.FeatureCards.Should().HaveCount(2);
            var card = actual.Homepage.FeatureCards[0];
            card.Id.Should().Be("1H202vCRDsn2RJjv7HMqkj");
            card.Heading.Should().Be("Is an apprenticeship right for you?");
            card.Description.Should().Be("Not sure about what to do?");
            card.Audience.Should().Be("Apprentices");
            card.LinkType.Should().Be("Internal");
            card.ExternalUrl.Should().BeNull();
            card.InternalPage.Slug.Should().Be("is-an-apprenticeship-right-for-you");
            card.InternalPage.Title.Should().Be("Check if an apprenticeship is right for you");
            card.InternalPage.Hub.Should().Be("Apprentices");
            card.InternalPage.PageType.Should().Be("Article");
            card.InternalPage.ParentSlug.Should().Be("are-they-right-for-you");
            card.IsExternal.Should().BeFalse();
            card.IsForEmployers.Should().BeFalse();
            card.Url.Should().Be("/apprentices/is-an-apprenticeship-right-for-you");
        }

        [Test]
        public void Then_An_External_Feature_Card_Is_Mapped()
        {
            var actual = JsonConvert.DeserializeObject<GetHomepageResponse>(Json);

            var card = actual.Homepage.FeatureCards[1];
            card.InternalPage.Should().BeNull();
            card.IsExternal.Should().BeTrue();
            card.IsForEmployers.Should().BeTrue();
            card.Url.Should().Be("https://www.findapprenticeship.service.gov.uk/");
        }
    }
}
