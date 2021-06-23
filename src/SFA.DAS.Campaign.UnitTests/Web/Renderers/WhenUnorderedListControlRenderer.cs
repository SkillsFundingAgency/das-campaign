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
    public  class WhenUnorderedListControlRenderer
    {
        [Test, MoqAutoData]
        public void IsPassedAnObjectOfIHtmlControlThatIsOfUnorderedListThenSupportsContentReturnsTrue(UnorderedList unorderedList, UnorderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(unorderedList);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void IsPassedAnObjectOfIHtmlControlThatIsNotOfUnorderedListThenSupportsContentReturnsFalse(Paragraph paragraph, UnorderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void IsPassedAnObjectOfUnorderedListThenRenderReturnsTheHtml(UnorderedListControlRenderer renderer)
        {
            var list = UnorderedListBuilder.New().AddItem("item 1").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ul><li>item 1</li></ul>");
        }
    }
}
