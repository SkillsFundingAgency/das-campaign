using System;
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
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.converters
{
    public class WhenConvertingTheAnnouncementJson
    {
        private const string Json = @"{
  ""announcement"": {
    ""mainContent"": {
      ""id"": ""4Kx1a2B3"",
      ""headline"": ""Apprenticeship funding changes from April 2026"",
      ""slug"": ""apprenticeship-funding-changes-from-april-2026"",
      ""intro"": ""What the new funding bands mean for employers."",
      ""publishedDate"": ""2026-03-02T00:00:00"",
      ""updatedDate"": ""2026-03-10T00:00:00"",
      ""audience"": ""Employers"",
      ""category"": ""Funding"",
      ""items"": [
        { ""values"": [ ""First paragraph."" ], ""type"": ""paragraph"", ""tableValue"": [], ""embeddedResource"": null },
        { ""values"": null, ""type"": ""unordered-list"", ""tableValue"": [ [ ""One"" ], [ ""Two"" ] ], ""embeddedResource"": null }
      ]
    },
    ""sections"": [
      {
        ""tabName"": ""What is changing"",
        ""tabTitle"": ""What is changing"",
        ""findTraineeship"": false,
        ""content"": { ""items"": [ { ""values"": [ ""Section paragraph."" ], ""type"": ""paragraph"", ""tableValue"": [], ""embeddedResource"": null } ] }
      }
    ]
  }
}";

        private const string ListJson = @"{
  ""announcements"": {
    ""announcements"": [
      { ""id"": ""a"", ""headline"": ""Newest"", ""slug"": ""newest"", ""intro"": ""Intro a"", ""publishedDate"": ""2026-03-02T00:00:00"", ""audience"": ""Employers"", ""category"": ""Funding"" },
      { ""id"": ""b"", ""headline"": ""Older"", ""slug"": ""older"", ""intro"": null, ""publishedDate"": null, ""audience"": ""Employers"", ""category"": null }
    ]
  }
}";

        [Test]
        public void Then_The_Announcement_Fields_Are_Mapped()
        {
            var actual = Convert(Json);

            actual.Should().NotBeNull();
            actual.Id.Should().Be("4Kx1a2B3");
            actual.Headline.Should().Be("Apprenticeship funding changes from April 2026");
            actual.Slug.Should().Be("apprenticeship-funding-changes-from-april-2026");
            actual.Intro.Should().Be("What the new funding bands mean for employers.");
            actual.PublishedDate.Should().Be(new DateTime(2026, 3, 2));
            actual.UpdatedDate.Should().Be(new DateTime(2026, 3, 10));
            actual.Audience.Should().Be("Employers");
            actual.Category.Should().Be("Funding");
            actual.Url.Should().Be("/employers/announcements/apprenticeship-funding-changes-from-april-2026");
        }

        [Test]
        public void Then_The_Content_Items_Are_Built_Into_Html_Controls()
        {
            var actual = Convert(Json);

            actual.Content.Should().HaveCount(2);
            actual.Content.First().Should().BeOfType<Paragraph>();
            actual.Content.Last().Should().BeOfType<UnorderedList>();
        }

        [Test]
        public void Then_The_Sections_Are_Built_As_Tabbed_Content()
        {
            var actual = Convert(Json);

            actual.Sections.Should().HaveCount(1);
            var section = actual.Sections.First();
            section.TabTitle.Should().Be("What is changing");
            section.Content.Should().HaveCount(1);
            section.Content.First().Should().BeOfType<Paragraph>();
        }

        [Test]
        public void Then_A_Null_Announcement_Returns_Null()
        {
            Convert(@"{ ""announcement"": null }").Should().BeNull();
        }

        [Test]
        public void Then_The_Converter_Only_Converts_Announcements()
        {
            var converter = new AnnouncementJsonConverter(new Mock<IHtmlControlAbstractFactory>().Object);

            converter.CanConvert(typeof(Announcement)).Should().BeTrue();
            converter.CanConvert(typeof(Page<Article>)).Should().BeFalse();
        }

        [Test]
        public void Then_The_Announcements_List_Deserialises()
        {
            var actual = JsonConvert.DeserializeObject<GetAnnouncementsResponse>(ListJson);

            actual.Announcements.Announcements.Should().HaveCount(2);
            var first = actual.Announcements.Announcements[0];
            first.Headline.Should().Be("Newest");
            first.PublishedDate.Should().Be(new DateTime(2026, 3, 2));
            first.Url.Should().Be("/employers/announcements/newest");
            actual.Announcements.Announcements[1].PublishedDate.Should().BeNull();
        }

        private static Announcement Convert(string json)
        {
            var factory = new HtmlControlAbstractFactory(new IHtmlControlFactory[]
            {
                new ParagraphControlFactory(),
                new UnorderedListControlFactory()
            });
            var converter = new AnnouncementJsonConverter(factory);

            return converter.ReadJson(new JsonTextReader(new StringReader(json)), typeof(Announcement), null, new JsonSerializer()) as Announcement;
        }
    }
}
