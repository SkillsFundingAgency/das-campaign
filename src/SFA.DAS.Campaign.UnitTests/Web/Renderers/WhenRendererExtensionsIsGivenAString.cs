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
        public void Then_If_It_Contains_Markup_A_Formed_Hyper_Link_Is_Returned()
        {
            var value = "[Scotland](/scotland/)";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("<a href=\"/scotland/\">Scotland</a>");
        }

        [Test]
        public void Then_If_It_Has_No_Markup_The_String_Is_Returned()
        {
            var value = "no markup";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("no markup");
        }

        [Test]
        public void Then_If_It_Contains_Markup_A_Formed_Hyper_Link_And_It_Is_An_External_Uri_Is_Returned_With_Target_Set()
        {
            var value = "[Scotland](https://www.apprenticeships.scot/)";

            var actual = value.CheckForAndConstructHyperlinks();
            actual.Should().Be("<a href=\"https://www.apprenticeships.scot/\" title=\"\" rel=\"external\" target=\"_blank\">Scotland</a>");
        }
    }
}
