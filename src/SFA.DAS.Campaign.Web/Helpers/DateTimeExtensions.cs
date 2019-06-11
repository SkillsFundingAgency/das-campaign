using System;

namespace SFA.DAS.Campaign.Web.Helpers
{
    public static class DateTimeExtensions
    {
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
