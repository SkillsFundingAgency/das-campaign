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
    public class WhenConvertingTheHubJson
    {
        private string jsonNoHeaderImage = "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"Find out how to become an apprentice, what apprenticeships are available and which employers offer them.\"},\"mainContent\":{\"headerImage\":null,\"cards\":[{\"pageType\":0,\"title\":\"Browse by interest\",\"metaDescription\":null,\"slug\":\"browse-by-interests\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what type of apprenticeships you can expect in your chosen interest.\"}]}, \"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";

        private const string json =
            "{\"hub\":{\"pageAttributes\":{\"pageType\":3,\"title\":\"Become an apprentice\",\"metaDescription\":\"something\",\"slug\":\"apprentices\",\"hubType\":\"Apprentices\",\"summary\":\"Find out how to become an apprentice, what apprenticeships are available and which employers offer them.\"},\"mainContent\":{\"headerImage\":{\"values\":null,\"type\":\"Asset\",\"tableValue\":null,\"embeddedResource\":{\"title\":\"apprentice-sparks\",\"id\":\"7FMiFuKxmMQDmVxbhPQy4K\",\"fileName\":\"apprentice-sparks.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/7FMiFuKxmMQDmVxbhPQy4K/2d693fc0e6955f6a58bc12e663282a80/apprentice-sparks.jpg\",\"size\":57643,\"description\":null}},\"cards\":[{\"pageType\":0,\"title\":\"Browse by interest\",\"metaDescription\":null,\"slug\":\"browse-by-interests\",\"hubType\":\"Apprentices\",\"summary\":\"Find out what type of apprenticeships you can expect in your chosen interest.\"}]},\"menuContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";


        [Test, MoqAutoData]
        public void The_Page_Model_Is_Returned(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }
        
        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_Page_Information(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Slug.Should().NotBeNullOrWhiteSpace();
            actual.Title.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.MetaDescription.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.PageTitle.Should().NotBeNullOrWhiteSpace();
        }

        [Test, MoqAutoData]
        public void The_Header_Image_Is_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.HeaderImage.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Cards_Are_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Cards.Should().NotBeNullOrEmpty();
        }

        [Test, MoqAutoData]
        public void If_The_Header_Image_Is_Null_Then_Header_Image_Is_Not_Set(HubJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter, jsonNoHeaderImage);

            actual.Content.HeaderImage.Description.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Url.Should().BeNullOrWhiteSpace();
            actual.Content.HeaderImage.Title.Should().BeNullOrWhiteSpace();
        }

        private static Page<Hub> InvokeReadJsonMethodOnConverter(HubJsonConverter converter, string jsonToUse = json)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(jsonToUse)), typeof(Page<Hub>), "",
                new Mock<JsonSerializer>().Object) as Page<Hub>;
            return actual;
        }
    }
}
