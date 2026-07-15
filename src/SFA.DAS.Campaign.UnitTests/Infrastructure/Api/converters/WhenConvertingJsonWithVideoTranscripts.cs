using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingJsonWithVideoTranscripts
    {
        // Mirrors the shape returned by the content API for the
        // apprentices/get-support-with-your-apprenticeship preview page:
        //  - a paragraph carrying a video transcript (empty text values)
        //  - an unordered-list whose "values" is null (the shape that used to NRE -> 500)
        private const string Json =
            "{\"article\":{\"pageAttributes\":{\"pageType\":2,\"title\":\"Get support\",\"metaDescription\":\"desc\",\"slug\":\"get-support-with-your-apprenticeship\",\"hubType\":\"Apprentices\",\"summary\":\"summary\"}," +
            "\"mainContent\":{\"items\":[" +
            "{\"values\":[\"\",\"\"],\"type\":\"paragraph\",\"tableValue\":[],\"embeddedResource\":null,\"videoTranscripts\":[{\"videoName\":\"Test video\",\"text\":\"\\nFirst line\\n\\nSecond line\"}]}," +
            "{\"values\":null,\"type\":\"unordered-list\",\"tableValue\":[[\"an item\"]],\"embeddedResource\":null,\"videoTranscripts\":null}" +
            "]}," +
            "\"menuContent\":{\"topLevel\":[],\"apprentices\":[],\"employers\":[],\"influencers\":[]}}}";

        private static ArticleJsonConverter BuildConverter()
        {
            var factories = new List<IHtmlControlFactory>
            {
                new ParagraphControlFactory(),
                new TableControlFactory(),
                new UnorderedListControlFactory(),
                new OrderedListControlFactory(),
                new HeadingControlFactory(),
                new ImageControlFactory(),
                new YouTubeControlFactory(),
                new BlockQuoteControlFactory(),
                new HorizontalRuleControlFactory()
            };

            return new ArticleJsonConverter(new HtmlControlAbstractFactory(factories));
        }

        private static Page<Article> Deserialize()
        {
            var converter = BuildConverter();
            return converter.ReadJson(new JsonTextReader(new StringReader(Json)), typeof(Page<Article>), "",
                new Mock<JsonSerializer>().Object) as Page<Article>;
        }

        [Test]
        public void The_Page_With_A_Null_Values_List_Item_Deserialises_Without_Throwing()
        {
            var actual = Deserialize();

            actual.Should().NotBeNull();
            actual.Content.PageControls.Should().NotBeEmpty();
        }

        [Test]
        public void The_Video_Transcript_Is_Mapped_Onto_The_Paragraph()
        {
            var actual = Deserialize();

            var paragraph = actual.Content.PageControls.OfType<Paragraph>().First();
            paragraph.VideoTranscripts.Should().ContainSingle();
            paragraph.VideoTranscripts.Single().VideoName.Should().Be("Test video");
        }

        [Test]
        public void The_Rendered_Page_Contains_The_Transcript_As_A_Details_Element()
        {
            var actual = Deserialize();

            var html = actual.Content.PageControls.ToHtmlForTest();

            html.Should().Contain("<details class=\"govuk-details fiu-details\" data-module=\"govuk-details\">");
            html.Should().Contain("<span class=\"govuk-details__summary-text\">Show transcript</span>");
            html.Should().Contain("First line");
            html.Should().Contain("Second line");
        }
    }

    internal static class RenderTestExtensions
    {
        public static string ToHtmlForTest(this IEnumerable<IHtmlControl> controls)
        {
            return new ControlsHtmlRenderer().ToHtml(controls).Value;
        }
    }
}
