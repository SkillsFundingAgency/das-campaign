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
    public class WhenTableControlRenderer
    {
        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Of_Table_Then_Supports_Content_Returns_True(Table table, TableControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(table);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_IHtmlControl_That_Is_Not_Of_Table_Then_Supports_Content_Returns_False(Paragraph paragraph, TableControlRenderer renderer)
        {
            var actual = renderer.SupportsContent(paragraph);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Table_Then_Render_Returns_The_Html(TableControlRenderer renderer)
        {
            var list = TableBuilder.New().AddHeading("heading 1").AddRowValue("value 1").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<table class=\"fiu-table\"><thead><tr><th>heading 1</th></tr></thead><tbody><tr><td>value 1</td></tr></tbody></table>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Table_With_Multiple_Rows_Then_Render_Returns_The_Html(TableControlRenderer renderer)
        {
            var list = TableBuilder.New().AddHeading("heading 1").AddRowValue("value 1").AddRowValue("value 2").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<table class=\"fiu-table\"><thead><tr><th>heading 1</th></tr></thead><tbody><tr><td>value 1</td></tr><tr><td>value 2</td></tr></tbody></table>");
        }

        [Test, MoqAutoData]
        public void Is_Passed_An_Object_Of_Table_With_Multiple_Rows_And_Columns_Then_Render_Returns_The_Html(TableControlRenderer renderer)
        {
            var list = TableBuilder.New().AddHeading("heading 1").AddHeading("heading 2").AddRowValue("value 1").AddRowValue("value 2").AddRowValue("value 3").AddRowValue("value 4").Build();
            var actual = renderer.Render(list);

            actual.Value.Should().NotBeNullOrWhiteSpace();
            actual.Value.Should().Be("<table class=\"fiu-table\"><thead><tr><th>heading 1</th><th>heading 2</th></tr></thead><tbody><tr><td>value 1</td><td>value 2</td></tr><tr><td>value 3</td><td>value 4</td></tr></tbody></table>");
        }
    }
}
