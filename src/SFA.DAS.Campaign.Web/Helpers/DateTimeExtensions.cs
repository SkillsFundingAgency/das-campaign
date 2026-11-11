using System;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class DateTimeExtensions
    {
        /// <summary>GOV.UK style long date, e.g. "10 November 2026".</summary>
        public const string GovUkDateFormat = "d MMMM yyyy";

        /// <summary>Machine-readable date for the HTML time element's datetime attribute.</summary>
        public const string IsoDateFormat = "yyyy-MM-dd";

        public static string ToGovUkDateString(this DateTime date) => date.ToString(GovUkDateFormat);

        public static string ToGovUkDateString(this DateTime? date) => date?.ToGovUkDateString();

        public static string ToIsoDateString(this DateTime date) => date.ToString(IsoDateFormat);

        public static string ToIsoDateString(this DateTime? date) => date?.ToIsoDateString();

        public static string ToFriendlyDaysAgo(this DateTime date)
        {
            var utcDateTime = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
            var daysAgo = (int) (DateTime.UtcNow.Date - utcDateTime).TotalDays;

            if (daysAgo > 7 || daysAgo < 0)
            {
                return utcDateTime.ToString("dd MMM yyyy");
            }

            switch (daysAgo)
            {
                case 0:
                    return "today";
                case 1:
                    return "yesterday";
                default:
                    return daysAgo + " days ago";
            }
        }
    }
}
