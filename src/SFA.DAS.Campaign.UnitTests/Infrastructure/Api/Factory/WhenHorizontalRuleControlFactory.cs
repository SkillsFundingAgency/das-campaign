using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WhenHorizontalRuleControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Horizontal_Rule_Then_Is_Valid_Returns_True(HorizontalRuleControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_Then_Is_Valid_Returns_False(HorizontalRuleControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_blockquote_Then_Create_Returns_BlockQuote(HorizontalRuleControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.Create(control) as HorizontalRule;

            actual.Should().NotBeNull();
        }

        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("hr")
                .AddEmptyTableValuesArray()
                .AddEmptyValuesArray()
               .Build();
            return control;
        }
    }
}
