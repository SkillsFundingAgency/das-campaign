using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.Web.Renderers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenHeadingControlRenderer
    {
        [Test, MoqAutoData]
        public void IsPassedAnObjectOfIHtmlControlThatIsOfParagraphThenSupportsContentReturnsTrue(Heading heading, HeadingControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(heading);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void IsPassedAnObjectOfIHtmlControlThatIsNotOfParagraphThenSupportsContentReturnsFalse(Table table, HeadingControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void IsPassedAnObjectOfHeadingThenRenderReturnsTheHtml(HeadingControlRenderer renderer)
        {
            var heading = new Heading
            {
                HeadingSize = 2
            };
            heading.Content.Add("a heading");
            
            var actual = renderer.Render(heading);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<h2>a heading</h2>");
        }
    }
}
