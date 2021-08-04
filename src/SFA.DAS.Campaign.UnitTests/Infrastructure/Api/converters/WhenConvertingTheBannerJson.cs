using System.IO;
using System.Linq;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingTheBannerJson
    {
        private const string json =
            "{\"banner\":{\"mainContent\":[{\"items\":[{\"values\":[\"[bold]Lorem ipsum dolor\",\" sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. \"],\"type\":\"paragraph\",\"tableValue\":[],\"embeddedResource\":null}],\"backgroundColour\":\"Yellow\",\"allowUserToHideTheBanner\":false,\"showOnTheHomepageOnly\":true,\"title\":\"Information\",\"id\":\"4UdDPEbvE3AVi8VNmb3wKh\"}]}}";


        [Test, MoqAutoData]
        public void The_Banner_Is_Returned(BannerJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Banner_Properties_Are_Set(BannerJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.BannerModels.Any().Should().BeTrue();
            actual.BannerModels.First().AllowUserToHideTheBanner.Should().BeFalse();
            actual.BannerModels.First().ShowOnTheHomepageOnly.Should().BeTrue();
            actual.BannerModels.First().Id.Should().NotBeNullOrWhiteSpace();
            actual.BannerModels.First().Title.Should().NotBeNullOrWhiteSpace();
            actual.BannerModels.First().BackgroundColour.Should().NotBeNullOrWhiteSpace();
            actual.BannerModels.First().Content.Any().Should().BeTrue();
        }
        
        private static Page<BannerContentType> InvokeReadJsonMethodOnConverter(BannerJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Page<BannerContentType>), "",
                new Mock<JsonSerializer>().Object) as Page<BannerContentType>;

            return actual;
        }
    }
}
