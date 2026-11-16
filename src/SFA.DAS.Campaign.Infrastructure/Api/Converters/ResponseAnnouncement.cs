using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class AnnouncementRoot
    {
        [JsonProperty("announcement")]
        public ResponseAnnouncement Announcement { get; set; }
    }

    public class ResponseAnnouncement
    {
        [JsonProperty("mainContent")]
        public AnnouncementContent MainContent { get; set; }

        [JsonProperty("sections")]
        public List<ResponseTabbedContent> Sections { get; set; }
    }

    public class AnnouncementContent
    {
        public AnnouncementContent()
        {
            Items = new List<Item>();
        }

        public string Id { get; set; }
        public string Headline { get; set; }
        public string Slug { get; set; }
        public string Intro { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Audience { get; set; }
        public string Category { get; set; }
        public List<Item> Items { get; set; }
    }
}
