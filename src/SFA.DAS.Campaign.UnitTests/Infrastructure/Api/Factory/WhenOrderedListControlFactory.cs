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
    public class WhenOrderedListControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Ordered_List_Then_Is_Valid_Returns_True(OrderedListControlFactory factory)
        {
            var control = BuildUnorderedListItemControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_The_Control_Then_Create_Returns_Ordered_List(OrderedListControlFactory factory)
        {
            var control = BuildUnorderedListItemControl();

            var actual = factory.Create(control) as OrderedList;

            actual.Should().NotBeNull();
            actual.Items.Any().Should().BeTrue();
        }

        private static Item BuildUnorderedListItemControl()
        {
            var control = new ItemBuilder().SetType("ordered-list").SetValues(new List<string>
            {
                "be 16 or over",
                "not already be in full-time education",
                "live in England"
            }).Build();
            return control;
        }
    }
}
