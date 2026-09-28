using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders;
using SFA.DAS.Campaign.Web.Renderers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenStatisticsControlRenderer
    {
        private const string StatisticItemHtml =
            "<li class=\"fiu-statistics__item\" role=\"listitem\"><div class=\"fiu-statistic\">" +
            "<p class=\"fiu-statistic__highlight\">86%</p>" +
            "<p class=\"fiu-statistic__text\">of employers developed skills</p>" +
            "<p class=\"fiu-statistic__reference\">Employer survey</p>" +
            "</div></li>";

        private const string QuoteItemHtml =
            "<li class=\"fiu-statistics__item\" role=\"listitem\"><figure class=\"fiu-quote fiu-statistic-quote\">" +
            "<blockquote class=\"fiu-quote__blockquote\">The best decision we made</blockquote>" +
            "<figcaption class=\"fiu-quote__caption\">Jane<span class=\"fiu-quote__caption-description\">Talent Specialist</span></figcaption>" +
            "</figure></li>";

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_HubStatistic_Then_Supports_Content_Returns_True(HubStatistic statistic, StatisticsControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(statistic);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_HubStatistic_Then_Supports_Content_Returns_False(Table table, StatisticsControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Statistic_Then_Render_Returns_The_Html(StatisticsControlRenderer renderer)
        {
            var actual = renderer.Render(BuildStatistic());

            actual.Value.Should().Be($"<ul class=\"fiu-statistics\" role=\"list\">{StatisticItemHtml}</ul>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Statistic_With_No_Highlight_Value_Then_Render_Returns_A_Quote(StatisticsControlRenderer renderer)
        {
            var actual = renderer.Render(BuildQuote());

            actual.Value.Should().Be($"<ul class=\"fiu-statistics\" role=\"list\">{QuoteItemHtml}</ul>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Statistic_Then_The_Content_Is_Html_Encoded(StatisticsControlRenderer renderer)
        {
            var statistic = BuildStatistic();
            statistic.Text = "<script>alert(1)</script>";

            var actual = renderer.Render(statistic);

            actual.Value.Should().NotContain("<script>");
        }

        [Test]
        public void And_Statistics_Follow_One_Another_Then_They_Are_Rendered_In_One_List()
        {
            var renderer = new ControlsHtmlRenderer();
            var controls = new List<IHtmlControl>
            {
                ParagraphBuilder.New().AddText("before").Build(),
                BuildStatistic(),
                BuildQuote(),
                ParagraphBuilder.New().AddText("after").Build(),
                BuildStatistic()
            };

            var actual = renderer.ToHtml(controls);

            actual.Value.Should().Be(
                "<p>before</p>" +
                $"<ul class=\"fiu-statistics\" role=\"list\">{StatisticItemHtml}{QuoteItemHtml}</ul>" +
                "<p>after</p>" +
                $"<ul class=\"fiu-statistics\" role=\"list\">{StatisticItemHtml}</ul>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_A_Paragraph_That_Only_Has_A_Statistic_Then_Only_The_Statistic_Is_Rendered(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("").Build();
            paragraph.StatsSection = BuildStatistic();

            var actual = renderer.Render(paragraph);

            actual.Value.Should().Be($"<ul class=\"fiu-statistics\" role=\"list\">{StatisticItemHtml}</ul>");
        }

        private static HubStatistic BuildStatistic()
        {
            return new HubStatistic
            {
                HighlightValue = "86%",
                Text = "of employers developed skills",
                ReferenceText = "Employer survey"
            };
        }

        private static HubStatistic BuildQuote()
        {
            return new HubStatistic
            {
                Text = "The best decision we made",
                QuoteName = "Jane",
                QuoteRole = "Talent Specialist"
            };
        }
    }
}
