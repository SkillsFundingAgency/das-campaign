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
            var value = "[Scotland](/scotland/)";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("<a href=\"/scotland/\">Scotland</a>");
        }

        [Test]
        public void ThenIfItHasNoMarkupTheStringIsReturned()
        {
            var value = "no markup";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("no markup");
        }

        [Test]
        public void ThenIfItContainsMarkupAFormedHyperLinkAndItIsAnExternalUriIsReturnedWithTargetSet()
        {
            var value = "[Scotland](https://www.apprenticeships.scot/)";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("<a href=\"https://www.apprenticeships.scot/\" title=\"\" rel=\"external\" target=\"_blank\">Scotland</a>");
        }
    }
}
