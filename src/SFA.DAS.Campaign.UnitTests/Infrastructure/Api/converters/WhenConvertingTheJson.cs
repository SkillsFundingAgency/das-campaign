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
    public class WhenConvertingTheJson
    {
        private const string json =
            "{\"PageAttributes\":{\"PageType\":2,\"Title\":\"Becoming an apprentice\",\"MetaDescription\":\"An apprenticeship gives you hands-on experience, a salary and the opportunity to train while you work as an apprentice.\",\"Slug\":\"becoming-apprentice\",\"HubType\":\"Apprentices\",\"Summary\":\"An apprenticeship is a real job where you learn, gain experience and get paid. You're an employee with a contract of employment and holiday leave.\"},\"MainContent\":{\"Items\":[{\"Values\":[\"To become an apprentice, you must:\"],\"Type\":\"paragraph\",\"TableValue\":[]}]}}";


        [Test, MoqAutoData]
        public void ThePageModelIsReturned(ArticleJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Page<Article>), "",
                new Mock<JsonSerializer>().Object);

            actual.Should().NotBeNull();
        }
    }
}
