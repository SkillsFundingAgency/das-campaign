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
        [TestCase("First line\nSecond line", "First line<br />Second line")]
        [TestCase("First line\r\nSecond line", "First line<br />Second line")]
        [TestCase("First line\rSecond line", "First line<br />Second line")]
        [TestCase("Blank line between\n\nthese two", "Blank line between<br /><br />these two")]
        public void Then_Line_Breaks_Are_Converted_To_Break_Tags(string value, string expected)
        {
            var actual = value.LineBreaksToHtml();

            actual.Should().Be(expected);
        }

        [TestCase("no line breaks")]
        [TestCase("")]
        [TestCase(null)]
        public void Then_A_String_Without_Line_Breaks_Is_Left_Alone(string value)
        {
            var actual = value.LineBreaksToHtml();

            actual.Should().Be(value);
        }

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

        [TestCase("https://www.apprenticeships.gov.uk/employer/find-apprenticeship-training")]
        [TestCase("http://www.apprenticeships.gov.uk/")]
        [TestCase("https://WWW.APPRENTICESHIPS.GOV.UK/apprentices")]
        public void Then_If_It_Contains_Markup_For_An_Apprenticeships_Gov_Uk_Uri_Then_No_Target_Is_Set(string url)
        {
            var value = $"[find apprenticeship training]({url})";

            var actual = value.CheckForAndConstructHyperlinks();

            actual.Should().Be($"<a href=\"{url}\">find apprenticeship training</a>");
        }

        [Test]
        public void Then_If_It_Contains_Markup_For_Bold_Then_The_String_Is_Correctly_Returned()
        {
            var value = "[bold]To support someone with apprenticeships you can: ";

            var actual = value.CheckForFontEffects();

            actual.Should().Be("<strong>To support someone with apprenticeships you can: </strong>");
        }

        [Test]
        public void Then_If_It_Contains_Markup_For_Italics_Then_The_String_Is_Correctly_Returned()
        {
            var value = "[italic]To support someone with apprenticeships you can: ";

            var actual = value.CheckForFontEffects();

            actual.Should().Be("<i>To support someone with apprenticeships you can: </i>");
        }
    }
}
