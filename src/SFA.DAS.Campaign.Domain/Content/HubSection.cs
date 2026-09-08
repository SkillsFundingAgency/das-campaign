using System;
using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class HubSection
    {
        public const string StatsSectionType = "Stats";

        public HubSection()
        {
            StepperLinks = new List<HubSectionLink>();
            StandardLinks = new List<HubSectionLink>();
            Statistics = new List<HubStatistic>();
        }
        
        public string SectionType { get; set; }

        public string Heading { get; set; }
        public string Introduction { get; set; }
        public Image Image { get; set; }
        public List<HubSectionLink> StepperLinks { get; set; }
        public List<HubSectionLink> StandardLinks { get; set; }
        public List<HubStatistic> Statistics { get; set; }
        public CtaPanel CtaPanel { get; set; }

        public bool IsStatsSection =>
            StatsSectionType.Equals(SectionType?.Trim(), StringComparison.OrdinalIgnoreCase);

        public bool HasContent =>
            !string.IsNullOrWhiteSpace(Heading)
            || !string.IsNullOrWhiteSpace(Introduction)
            || StepperLinks.Count > 0
            || StandardLinks.Count > 0
            || Statistics.Count > 0
            || CtaPanel != null;
    }

    public class HubSectionLink : Card
    {
        public CtaPanel CtaPanel { get; set; }
    }

    public class HubStatistic
    {
        public string Text { get; set; }
        public string HighlightValue { get; set; }
        public string QuoteName { get; set; }
        public string QuoteRole { get; set; }
        public string ReferenceText { get; set; }

        // A highlight value is what makes an entry a statistic. Without one the CMS is giving us a quote.
        public bool IsStat => !string.IsNullOrWhiteSpace(HighlightValue);

        public bool HasContent =>
            IsStat
            || !string.IsNullOrWhiteSpace(Text)
            || !string.IsNullOrWhiteSpace(QuoteName)
            || !string.IsNullOrWhiteSpace(QuoteRole);
    }

    public class CtaPanel
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ButtonText { get; set; }
        public string Url { get; set; }
    }
}
