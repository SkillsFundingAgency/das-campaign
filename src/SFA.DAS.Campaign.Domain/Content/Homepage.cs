using System;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Homepage : IContentType
    {
        public string PageTitle { get; set; }
        public string BrowserTitle { get; set; }
        public string IntroText { get; set; }
        public List<FeatureCard> FeatureCards { get; set; } = new List<FeatureCard>();
    }

    public class FeatureCard
    {
        public string Id { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Audience { get; set; }
        public string LinkType { get; set; }
        public string ExternalUrl { get; set; }
        public FeatureCardInternalPage InternalPage { get; set; }

        public bool IsExternal => string.Equals(LinkType, "External", StringComparison.OrdinalIgnoreCase);

        public bool IsForEmployers => string.Equals(Audience, "Employers", StringComparison.OrdinalIgnoreCase);

        public string Url => IsExternal ? ExternalUrl : InternalPage?.Url;
    }

    public class FeatureCardInternalPage
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Hub { get; set; }
        public string PageType { get; set; }
        public string ParentSlug { get; set; }

        public string Url
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Hub))
                {
                    return null;
                }

                var hub = Hub.ToLowerInvariant();

                if (string.Equals(PageType, "Hub", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(Slug))
                {
                    return $"/{hub}";
                }

                return $"/{hub}/{Slug}";
            }
        }
    }
}
