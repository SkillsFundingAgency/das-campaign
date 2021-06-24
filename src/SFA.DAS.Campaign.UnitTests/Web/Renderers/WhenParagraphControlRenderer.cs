using System;
using System.Collections.Generic;
using System.Text;
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
        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Paragraph_Then_Supports_Content_Returns_True(Paragraph paragraph, ParagraphControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Paragraph_Then_Supports_Content_Returns_False(Table table, ParagraphControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Paragraph_Then_Render_Returns_The_Html(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a line").Build();
            var actual = renderer.Render(paragraph);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>a line</p>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Paragraph_With_Multiple_Items_Then_Render_Returns_The_Html(ParagraphControlRenderer renderer)
        {
            var paragraph = ParagraphBuilder.New().AddText("a word ").AddText("another word").Build();
            var actual = renderer.Render(paragraph);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<p>a word another word</p>");
        }
    }
}
