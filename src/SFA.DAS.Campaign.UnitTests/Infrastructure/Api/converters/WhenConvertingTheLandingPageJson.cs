using System.IO;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingTheLandingPageJson
    {
        private const string json =
            "{\"landingPage\":{\"pageAttributes\":{\"id\":null,\"pageType\":1,\"title\":\"How do they work?\",\"metaDescription\":\"something\",\"slug\":\"how-do-they-work\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what to expect and the application process.\"},\"mainContent\":{\"headerImage\":{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"apprentice-ekansh\",\"id\":\"7iiurFb6uR77HGJq0HRsN3\",\"fileName\":\"apprentice-ekansh.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/7iiurFb6uR77HGJq0HRsN3/bbdae1064ce5e42d81c9c3655db1b561/apprentice-ekansh.jpg\",\"size\":50339,\"description\":null}},\"cards\":[{\"landingPage\":{\"slug\":\"how-do-they-work\",\"title\":\"How do they work?\",\"hub\":\"Apprentices\"},\"id\":\"76YH23UZsD6k196MZb6AAI\",\"pageType\":0,\"title\":\"Becoming an apprentice\",\"metaDescription\":\"An apprenticeship gives you hands-on experience, a salary and the opportunity to train while you work as an apprentice.\",\"slug\":\"becoming-apprentice\",\"hubType\":\"Apprentices\",\"summary\":\"An apprenticeship is a real job where you learn, gain experience and get paid. You're an employee with a contract of employment and holiday leave.\"}]}}}";

        private const string jsonNoHeaderImage =
            "{\"landingPage\":{\"pageAttributes\":{\"id\":null,\"pageType\":1,\"title\":\"How do they work?\",\"metaDescription\":\"something\",\"slug\":\"how-do-they-work\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what to expect and the application process.\"},\"mainContent\":{\"headerImage\":null},\"cards\":[{\"landingPage\":{\"slug\":\"how-do-they-work\",\"title\":\"How do they work?\",\"hub\":\"Apprentices\"},\"id\":\"76YH23UZsD6k196MZb6AAI\",\"pageType\":0,\"title\":\"Becoming an apprentice\",\"metaDescription\":\"An apprenticeship gives you hands-on experience, a salary and the opportunity to train while you work as an apprentice.\",\"slug\":\"becoming-apprentice\",\"hubType\":\"Apprentices\",\"summary\":\"An apprenticeship is a real job where you learn, gain experience and get paid. You're an employee with a contract of employment and holiday leave.\"}]}}";

        [Test, MoqAutoData]
        public void The_Page_Model_Is_Returned(LandingPageJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }
        
        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_Page_Information(LandingPageJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Slug.Should().NotBeNullOrWhiteSpace();
            actual.Title.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.MetaDescription.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.PageTitle.Should().NotBeNullOrWhiteSpace();
        }

        [Test, MoqAutoData]
        public void The_Header_Image_Is_Set(LandingPageJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.HeaderImage.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Cards_Are_Set(LandingPageJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Cards.Should().NotBeNullOrEmpty();
        }

        [Test, MoqAutoData]
        public void If_The_Header_Image_Is_Null_Then_Header_Image_Is_Not_Set(LandingPageJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonNoHeaderImage);

            actual.Content.HeaderImage.Description.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Url.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Title.Should().BeNullOrWhiteSpace();
        }

        private static Page<LandingPage> InvokeReadJsonMethodOnConverter(LandingPageJsonConverter converter, string jsonToUse = json)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(jsonToUse)), typeof(Page<LandingPage>), "",
                new Mock<JsonSerializer>().Object) as Page<LandingPage>;
            return actual;
        }
    }
}
