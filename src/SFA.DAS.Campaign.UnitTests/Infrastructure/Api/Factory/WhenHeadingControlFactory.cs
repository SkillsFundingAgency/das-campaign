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
    public class WhenHeadingControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Heading_Then_Is_Valid_Returns_True(HeadingControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Heading_Then_Create_Returns_Heading(HeadingControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.Create(control) as Heading;

            actual.Should().NotBeNull();
            actual.Content.Any().Should().BeTrue();
            actual.HeadingSize.Should().Be(2);
        }

        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("heading-2")
                .SetValue("What is an apprenticeship?").Build();
            return control;
        }
    }
}
