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
            "{\"article\":{\"pageAttributes\":{\"pageType\":2,\"title\":\"Becoming an apprentice\",\"metaDescription\":\"An apprenticeship gives you hands-on experience, a salary and the opportunity to train while you work as an apprentice.\",\"slug\":\"becoming-apprentice\",\"hubType\":\"Apprentices\",\"summary\":\"An apprenticeship is a real job where you learn, gain experience and get paid. You're an employee with a contract of employment and holiday leave.\"},\"mainContent\":{\"items\":[{\"values\":[\"To become an apprentice, you must:\"],\"type\":\"paragraph\",\"tableValue\":[]}]},\"relatedArticles\":[{\"pageType\":0,\"title\":\"Applying for an apprenticeship\",\"metaDescription\":\"From finding the right apprenticeship and employer and how to write a good CV, covering letter and what to expect from the employer.\",\"slug\":\"applying-apprenticeship\",\"hubType\":\"Apprentices\",\"summary\":\"There are hundreds of different apprenticeships to choose from. To apply for one, you’ll need to create an account on the find an apprenticeship service. You can also save any apprenticeships you like and then apply for them later.\"}]}}";


        [Test, MoqAutoData]
        public void The_Page_Model_Is_Returned(ArticleJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Should().NotBeNull();
        }


        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_Page_Information(ArticleJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Slug.Should().NotBeNullOrWhiteSpace();
            actual.Title.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.MetaDescription.Should().NotBeNullOrWhiteSpace();
            actual.MetaContent.PageTitle.Should().NotBeNullOrWhiteSpace();
        }

        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_Related_Page_Information(ArticleJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.RelatedPages.Should().NotBeNullOrEmpty();
        }

        [Test, MoqAutoData]
        public void The_Page_Model_Is_Populated_With_The_Page_Controls(ArticleJsonConverter converter)
        {
            var actual = InvokeReadJsonMethodOnConverter(converter);

            actual.Content.PageControls.Should().NotBeNullOrEmpty();
        }

        private static Page<Article> InvokeReadJsonMethodOnConverter(ArticleJsonConverter converter)
        {
            var actual = converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Page<Article>), "",
                new Mock<JsonSerializer>().Object) as Page<Article>;
            return actual;
        }
    }
}
