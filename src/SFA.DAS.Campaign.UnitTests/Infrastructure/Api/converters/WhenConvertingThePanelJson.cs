using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Testing.AutoFixture;
using System.IO;
using System.Linq;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingThePanelJson
    {
        private const string json =
            "{\"panel\":{\"mainContent\":{\"title\":\"Are you ready to get going?\",\"slug\":\"are-you-ready-to-get-going\",\"items\":[{\"values\":[\"Hire an apprentice and invest in the future, by getting talent that’s right for you. It’s easier than you think: if you are a smaller employer, you are eligible for government funding which will pay between 95% and 100% of the apprentice training costs. If you are a large employer, you can use your levy to pay for your apprenticeship training.\"],\"type\":\"paragraph\",\"tableValue\":[],\"embeddedResource\":null},{\"values\":[\"Apprenticeships are suitable for people at any level so you can hire someone new or upskill an existing employee, allowing you to grow talent and develop a motivated, skilled and qualified workforce. They help you to:\"],\"type\":\"paragraph\",\"tableValue\":[],\"embeddedResource\":null},{\"values\":null,\"type\":\"unordered-list\",\"tableValue\":[[\"Offer on-the-job training in a safe working environment\"],[\"Build a culture of learning and development\"],[\"Enjoy higher staff retention and morale\"]],\"embeddedResource\":null},{\"values\":[\"“Have a plan of what it is that you would like for your student to learn while they are with you, speak to them about what their goals are. Be ready to offer extra time and support to make sure that you get the best out of them.”\",\"\\n\",\"[bold]Employer\",\"\\nOrganisation\"],\"type\":\"blockquote\",\"tableValue\":[],\"embeddedResource\":null},{\"values\":[\"\"],\"type\":\"paragraph\",\"tableValue\":[],\"embeddedResource\":null}],\"image\":{\"title\":\"Funding Tool Panel One Image\",\"id\":\"6i8cKkZ5oSqK2Sr0jhfmTj\",\"fileName\":\"image1.jpg\",\"contentType\":\"image/jpeg\",\"url\":\"https://images.ctfassets.net/8kbr1n52z8s2/6i8cKkZ5oSqK2Sr0jhfmTj/a344428166d3ae0ad059f39edb5cc562/image1.jpg\",\"size\":2230128,\"description\":\"\"},\"button\":{\"title\":null,\"url\":null,\"styles\":null},\"linkTitle\":\"Get going\"},\"id\":3}}";

        [Test, MoqAutoData]
        public void The_Panel_Is_Returned(PanelJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void The_Panel_Properties_Are_Set(PanelJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Title.Should().NotBeNullOrWhiteSpace();
            actual.LinkTitle.Should().NotBeNullOrWhiteSpace();
            actual.Slug.Should().NotBeNullOrWhiteSpace();
            actual.Content.Any().Should().BeTrue();
            actual.Id.Should().NotBe(null);
        }

        private static Panel InvokeReadJsonMethodOnConverter(PanelJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Panel), " ", new Mock<JsonSerializer>().Object) as Panel;

            return actual;
        }
    }
}
