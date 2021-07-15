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
    public class WhenConvertingTheMenuJson
    {
        private const string json =
            "{\"menu\":{\"mainContent\":{\"topLevel\":[{\"slug\":\"apprentices\",\"title\":\"Become an apprentice\",\"hub\":\"Apprentices\",\"pageType\":\"Hub\"}],\"apprentices\":[{\"slug\":\"are-they-right-for-you\",\"title\":\"Are they right for you?\",\"hub\":\"Apprentices\",\"pageType\":\"LandingPage\"}],\"employers\":[{\"slug\":\"are-they-right-for-you-employers\",\"title\":\"Are they right for you?\",\"hub\":\"Employers\",\"pageType\":\"LandingPage\"}],\"influencers\":[{\"slug\":\"how-they-work\",\"title\":\"How they work\",\"hub\":\"Influencers\",\"pageType\":\"LandingPage\"}]}}}";


        [Test, MoqAutoData]
        public void The_Menu_Is_Returned(MenuJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Urls_Are_Set(MenuJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Menu.Apprentices.Should().NotBeNullOrEmpty();
            actual.Menu.Employers.Should().NotBeNullOrEmpty();
            actual.Menu.Influencers.Should().NotBeNullOrEmpty();
            actual.Menu.TopLevel.Should().NotBeNullOrEmpty();
        }
        
        private static Page<Menu> InvokeReadJsonMethodOnConverter(MenuJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Page<Menu>), "",
                new Mock<JsonSerializer>().Object) as Page<Menu>;

            return actual;
        }
    }
}
