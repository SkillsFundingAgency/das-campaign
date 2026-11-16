using System;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class AnnouncementSummary
    {
        public string Id { get; set; }
        public string Headline { get; set; }
        public string Slug { get; set; }
        public string Intro { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Audience { get; set; }
        public string Category { get; set; }

        public string Url => string.IsNullOrWhiteSpace(Slug) ? Announcement.ListPageUrl : $"{Announcement.ListPageUrl}/{Slug}";
    }
}
