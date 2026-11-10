using System;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Web.Helpers;

namespace SFA.DAS.Campaign.UnitTests.Web.Helpers
{
    public class WhenFormattingDates
    {
        private static readonly DateTime Date = new DateTime(2026, 11, 3);

        [Test]
        public void Then_A_Date_Is_Formatted_In_The_GovUk_Style()
        {
            Date.ToGovUkDateString().Should().Be("3 November 2026");
        }

        [Test]
        public void Then_A_Nullable_Date_With_A_Value_Is_Formatted_In_The_GovUk_Style()
        {
            ((DateTime?)Date).ToGovUkDateString().Should().Be("3 November 2026");
        }

        [Test]
        public void Then_A_Null_Date_Formats_To_Null_Rather_Than_Throwing()
        {
            ((DateTime?)null).ToGovUkDateString().Should().BeNull();
            ((DateTime?)null).ToIsoDateString().Should().BeNull();
        }

        [Test]
        public void Then_A_Date_Is_Formatted_As_An_Iso_Date()
        {
            Date.ToIsoDateString().Should().Be("2026-11-03");
            ((DateTime?)Date).ToIsoDateString().Should().Be("2026-11-03");
        }
    }
}
