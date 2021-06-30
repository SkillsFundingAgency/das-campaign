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
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Unordered_List_Then_Supports_Content_Returns_True(UnorderedList unorderedList, UnorderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(unorderedList);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Unordered_List_Then_Supports_Content_Returns_False(Paragraph paragraph, UnorderedListControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Unordered_List_Then_Render_Returns_The_Html(UnorderedListControlRenderer renderer)
        {
            var list = UnorderedListBuilder.New().AddItem("item 1").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ul><li>item 1</li></ul>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Unordered_List_With_Multiple_Items_Then_Render_Returns_The_Html(UnorderedListControlRenderer renderer)
        {
            var list = UnorderedListBuilder.New().AddItem("item 1").AddItem("item 2").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ul><li>item 1</li><li>item 2</li></ul>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Unordered_List_With_A_HyperLink_Then_Render_Returns_The_Html(UnorderedListControlRenderer renderer)
        {
            var list = UnorderedListBuilder.New().AddItem("item 1 [find apprenticeship training](https://www.apprenticeships.gov.uk/employer/find-apprenticeship-training)").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<ul><li>item 1 <a href=\"https://www.apprenticeships.gov.uk/employer/find-apprenticeship-training\" title=\"\" rel=\"external\" target=\"_blank\">find apprenticeship training</a></li></ul>");
        }
    }
}
