using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenTableFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Paragraph_With_Table_Value_Then_Is_Valid_Returns_True(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.IsValid(control);
            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Paragraph_With_No_Table_Values_Then_Is_Valid_Returns_False(TableControlFactory factory)
        {
            var control = new ItemBuilder()
                .SetType("paragraph")
                .Build();

            var actual = factory.IsValid(control);
            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_Table(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.Create(control) as Table;
            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_Table_And_The_Header_Columns_Are_Populated(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.Create(control) as Table;
            actual.Should().NotBeNull();
            actual.Headings.Count.Should().BeGreaterThan(0);
            actual.Headings[0].Should().Be("");
            actual.Headings[1].Should().Be("Level");
            actual.Headings[2].Should().Be("Equivalent education level");
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_Table_And_The_Row_Columns_Are_Populated(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.Create(control) as Table;
            actual.Should().NotBeNull();
            actual.Rows.Count.Should().BeGreaterThan(0);
            actual.Rows[0].Trim().Should().Be("Intermediate");
            actual.Rows[1].Should().Be("2");
            actual.Rows[2].Should().Be("GCSE");
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Table_With_Table_Value_Then_Is_Valid_Returns_True(TableControlFactory factory)
        {
            var control = BuildNativeTableControl(false, false);

            var actual = factory.IsValid(control);
            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Table_With_No_Header_Row_Then_Every_Row_Is_A_Body_Row(TableControlFactory factory)
        {
            var control = BuildNativeTableControl(false, false);

            var actual = factory.Create(control) as Table;

            actual.Should().NotBeNull();
            actual.Headings.Should().BeEmpty();
            actual.ColumnCount.Should().Be(2);
            actual.HasHeaderColumn.Should().BeFalse();
            actual.Rows.Should().Equal("Apprenticeships", "12 months", "Foundation apprenticeships", "8 months");
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Table_With_A_Header_Row_And_Column_Then_They_Are_Mapped(TableControlFactory factory)
        {
            var control = BuildNativeTableControl(true, true);

            var actual = factory.Create(control) as Table;

            actual.Should().NotBeNull();
            actual.Headings.Should().Equal("Apprenticeships", "12 months");
            actual.HasHeaderColumn.Should().BeTrue();
            actual.Rows.Should().Equal("Foundation apprenticeships", "8 months");
        }

        private static Item BuildNativeTableControl(bool hasHeaderRow, bool hasHeaderColumn)
        {
            return new ItemBuilder()
                .SetType("table")
                .SetTableHeaders(hasHeaderRow, hasHeaderColumn)
                .SetTableValue(new List<string> { "Apprenticeships", "12 months" })
                .SetTableValue(new List<string> { "Foundation apprenticeships", "8 months" })
                .Build();
        }

        private static Item BuildTableControl()
        {
            var control = new ItemBuilder()
                .SetType("paragraph")
                .SetTableValue(new List<string>
                {
                    "",
                    "Level",
                    "Equivalent education level"
                })
                .SetTableValue(new List<string>
                {
                    "Intermediate ",
                    "2",
                    "GCSE"
                }).Build();
            return control;
        }
    }
}
