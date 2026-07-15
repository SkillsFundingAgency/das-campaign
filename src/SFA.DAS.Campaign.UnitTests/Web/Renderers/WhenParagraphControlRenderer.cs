using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.UnitTests.Web.Renderers.Builders;
using SFA.DAS.Campaign.Web.Renderers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenParagraphControlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Paragraph_Then_Supports_Content_Returns_True(Paragraph paragraph, ParagraphControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Paragraph_Then_Supports_Content_Returns_False(Table table, ParagraphControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Paragraph_Then_Render_Returns_The_Html(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a line").Build();
            var actual = renderer.Render(paragraph);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>a line</p>");
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_Paragraph_With_Multiple_Items_Then_Render_Returns_The_Html(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a word ").AddText("another word").Build();
            var actual = renderer.Render(paragraph);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>a word another word</p>");
        }

        [Test, AutoData]
        public void Is_Passed_A_Paragraph_With_A_Video_Transcript_Then_Render_Returns_A_Details_Element(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("")
                .AddVideoTranscript("Test video", "First line\n\nSecond line").Build();

            var actual = renderer.Render(paragraph);

            actual.Value.Should().Contain("<details class=\"govuk-details fiu-details\" data-module=\"govuk-details\">");
            actual.Value.Should().Contain("<span class=\"govuk-details__summary-text\">Show transcript</span>");
            actual.Value.Should().Contain("<p class=\"govuk-body\">First line</p>");
            actual.Value.Should().Contain("<p class=\"govuk-body\">Second line</p>");
        }

        [Test, AutoData]
        public void Is_Passed_A_Paragraph_With_No_Video_Transcripts_Then_Render_Does_Not_Add_A_Details_Element(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a line").Build();

            var actual = renderer.Render(paragraph);

            actual.Value.Should().Be("<p>a line</p>");
        }
    }
}
