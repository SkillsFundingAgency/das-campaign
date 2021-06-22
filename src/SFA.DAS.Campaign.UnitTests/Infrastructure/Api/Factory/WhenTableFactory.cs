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
        public void IsGivenAnItemOfParagraphWithTableValueThenIsValidReturnsTrue(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.IsValid(control);
            actual.Should().BeTrue();
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

        [Test, MoqAutoData]
        public void IsGivenAnItemOfParagraphWithNoTableValuesThenIsValidReturnsFalse(TableControlFactory factory)
        {
            var control = new ItemBuilder()
                .SetType("paragraph")
                .Build();

            var actual = factory.IsValid(control);
            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void IsGivenTheControlThenCreateReturnsTable(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.Create(control) as Table;
            actual.Should().NotBeNull();
        }

        [Test, MoqAutoData]
        public void IsGivenTheControlThenCreateReturnsTableAndTheHeaderColumnsArePopulated(TableControlFactory factory)
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
        public void IsGivenTheControlThenCreateReturnsTableAndTheRowColumnsArePopulated(TableControlFactory factory)
        {
            var control = BuildTableControl();

            var actual = factory.Create(control) as Table;
            actual.Should().NotBeNull();
            actual.Rows.Count.Should().BeGreaterThan(0);
            actual.Rows[0].Trim().Should().Be("Intermediate");
            actual.Rows[1].Should().Be("2");
            actual.Rows[2].Should().Be("GCSE");
        }
    }
}
