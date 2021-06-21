using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Factory;
using SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory.Builders;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Factory
{
    public class WhenGivenAnItem
    {
        private HtmlControlAbstractFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new HtmlControlAbstractFactory(new []{new ParagraphControlFactory()});
        }

        [Test]
        public void AndTheItemIsOfTypeParagraphThenAParagraphIsReturned()
        {
            var actual = _factory.CreateControlFactoryFor(new ItemBuilder().SetType("paragraph").SetValues(
                new List<string>
                {
                    "To become an apprentice, you must:"
                }).Build());

            actual.Should().NotBeNull();
        }
    }
}
