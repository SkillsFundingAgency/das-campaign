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
    public  class WhenOrderedListControlRenderer
    {
        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Ordered_List_Then_Supports_Content_Returns_True(OrderedList unorderedList, OrderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(unorderedList);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Ordered_List_Then_Supports_Content_Returns_False(Paragraph paragraph, OrderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Ordered_List_Then_Render_Returns_The_Html(OrderedListControlRenderer renderer)
        {
            var list = OrderedListBuilder.New().AddItem("item 1").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ol><li>item 1</li></ol>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Unordered_List_With_Multiple_Items_Then_Render_Returns_The_Html(OrderedListControlRenderer renderer)
        {
            var list = OrderedListBuilder.New().AddItem("item 1").AddItem("item 2").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ol><li>item 1</li><li>item 2</li></ol>");
        }
    }
}
