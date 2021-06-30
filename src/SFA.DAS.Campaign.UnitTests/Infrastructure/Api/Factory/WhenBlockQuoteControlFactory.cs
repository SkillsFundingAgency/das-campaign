using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenBlockQuoteControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_blockquote_Then_Is_Valid_Returns_True(BlockQuoteControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_blockquote_Then_Is_Valid_Returns_False(BlockQuoteControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_blockquote_Then_Create_Returns_BlockQuote(BlockQuoteControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.Create(control) as BlockQuote;

            actual.Should().NotBeNull();
            actual.Content.Any().Should().BeTrue();
        }

        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("blockquote")
                .SetValue("This is a blockquote").Build();
            return control;
        }
    }
}
