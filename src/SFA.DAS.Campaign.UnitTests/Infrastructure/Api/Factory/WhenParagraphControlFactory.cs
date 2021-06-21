using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenParagraphControlFactory
    {
        [Test]
        public void IsGivenAnItemOfTypeParagraphThenIsValidReturnsTrue()
        {
            var factory = new ParagraphControlFactory();
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ").Build();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test]
        public void IsGivenAnItemOfTypeParagraphAndTableValueIsEmptyThenIsValidReturnsTrue()
        {
            var factory = new ParagraphControlFactory();
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeTrue();
        }

        [Test]
        public void IsGivenAnItemOfTypeParagraphAndTableValueIsNotEmptyThenIsValidReturnsFalse()
        {
            var factory = new ParagraphControlFactory();
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.IsValid(control);

            actual.Should().BeFalse();
        }

        [Test]
        public void IsGivenAnItemOfTypeParagraphAndTableValueIsNotEmptyThenCreateReturnsParagraph()
        {
            var factory = new ParagraphControlFactory();
            var control = new ItemBuilder().SetType("paragraph")
                .SetValue("To become an apprentice, you must: ")
                .SetTableValue(new List<string>())
                .Build();

            var actual = factory.Create(control) as Paragraph;

            actual.Should().NotBeNull();
            actual.Content.Any().Should().BeTrue();
        }
    }
}
