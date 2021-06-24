using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
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
    public class WhenParagraphControlFactory
    {
        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_Then_Is_Valid_Returns_True(ParagraphControlFactory factory)
        {
            var control = BuildControl();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And_Table_Value_Is_Empty_Then_Is_Valid_Returns_True(ParagraphControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And_Table_Value_Is_Not_Empty_Then_Is_Valid_Returns_False(ParagraphControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test, MoqAutoData]
        public void Is_Given_An_Item_Of_Type_Paragraph_And_Table_Value_Is_Not_Empty_Then_Create_Returns_Paragraph(ParagraphControlFactory factory)
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.Create(control) as Paragraph;

            actual.Should().NotBeNull();
            actual.Content.Any().Should().BeTrue();
        }

        private static Item BuildControl()
        {
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ").Build();
            return control;
        }
    }
}
