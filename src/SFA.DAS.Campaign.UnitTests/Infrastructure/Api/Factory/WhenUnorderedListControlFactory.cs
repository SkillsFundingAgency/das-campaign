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

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenUnorderedListControlFactory
    {
        [Test]
        public void IsGivenAnItemOfTypeUnorderedListThenIsValidReturnsTrue()
        {
            var factory = new UnorderedListControlFactory();
            var control = BuildUnorderedListItemControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        private static Item BuildUnorderedListItemControl()
        {
            var control = new ItemBuilder().SetType("unordered-list").SetValues(new List<string>
            {
                "be 16 or over",
                "not already be in full-time education",
                "live in England"
            }).Build();
            return control;
        }

        [Test]
        public void IsGivenTheControlThenCreateReturnsUnOrderedList()
        {
            var factory = new UnorderedListControlFactory();
            var control = BuildUnorderedListItemControl();

            var actual = factory.Create(control) as UnorderedList;

            actual.Should().NotBeNull();
            actual.Items.Any().Should().BeTrue();
        }
    }
}
