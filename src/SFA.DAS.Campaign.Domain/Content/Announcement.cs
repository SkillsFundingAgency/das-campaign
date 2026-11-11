using System;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Announcement : IContentType
    {
        public const string ListPageUrl = "/employers/announcements";

        public Announcement()
        {
            Content = new List<IHtmlControl>();
            Sections = new List<TabbedContent>();
        }

        public string Id { get; set; }
        public string Headline { get; set; }
        public string Slug { get; set; }
        public string Intro { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Audience { get; set; }
        public string Category { get; set; }
        public IEnumerable<IHtmlControl> Content { get; set; }
        public IEnumerable<TabbedContent> Sections { get; set; }

        public string Url => string.IsNullOrWhiteSpace(Slug) ? ListPageUrl : $"{ListPageUrl}/{Slug}";
    }
}
