using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenBlockQuoteControlRenderer
    {
        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_BlockQuote_Then_Supports_Content_Returns_True(BlockQuote quote, BlockQuoteControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(quote);

            actual.Should().BeTrue();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_BlockQuote_Then_Supports_Content_Returns_False(Table table, BlockQuoteControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, AutoData]
        public void Is_Passed_An_Object_Of_BlockQuote_Then_Render_Returns_The_Html(BlockQuoteControlRenderer renderer)
        {
            var blockquote = BuildBlockQuote();

            var actual = renderer.Render(blockquote);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<blockquote>a block quote</blockquote>");
        }

        private static BlockQuote BuildBlockQuote()
        {
            var heading = new BlockQuote
            {
                Content = new List<string>()
            };
            heading.Content.Add("a block quote");
            return heading;
        }


        [Test, AutoData]
        public void Is_Passed_An_Object_Of_BlockQuote_With_Multiple_Items_Then_Render_Returns_The_Html(BlockQuoteControlRenderer renderer)
        {
            var blockquote = BuildBlockQuote();
            blockquote.Content.Add("line 2");
            var actual = renderer.Render(blockquote);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<blockquote>a block quote<br />line 2</blockquote>");
        }
    }
}
