using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Renderers;

namespace SFA.DAS.Campaign.UnitTests.Web.Renderers
{
    public class WhenRendererExtensionsIsGivenAString
    {
        [Test]
        public void ThenIfItContainsMarkupAFormedHyperLinkIsReturned()
        {
            var value = "[Scotland](https://www.apprenticeships.scot/)";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("<a href=\"https://www.apprenticeships.scot/\">Scotland</a>");
        }

        [Test]
        public void ThenIfItHasNoMarkupTheStringIsReturned()
        {
            var value = "no markup";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("no markup");
        }
    }
}
