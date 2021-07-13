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
    public class WhenConvertingTheSiteMapJson
    {
        private const string json =
            "{\"map\":{\"mainContent\":{\"pages\":[{\"slug\":\"useful-resources-for-apprentices\",\"title\":\"Useful resources\",\"hub\":\"Apprentices\",\"pageType\":\"LandingPage\"},{\"slug\":\"how-they-work\",\"title\":\"How they work\",\"hub\":\"Influencers\",\"pageType\":\"LandingPage\"}]}}}";


        [Test, MoqAutoData]
        public void The_Site_Map_Is_Returned(SiteMapJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Urls_Are_Set(SiteMapJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.Urls.Should().NotBeNullOrEmpty();
        }
        
        private static Page<SiteMap> InvokeReadJsonMethodOnConverter(SiteMapJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Page<SiteMap>), "",
                new Mock<JsonSerializer>().Object) as Page<SiteMap>;
            return actual;
        }
    }
}
