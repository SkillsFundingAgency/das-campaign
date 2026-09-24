using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseHubSection
    {
        [JsonProperty("sectionType")]
        public string SectionType { get; set; }

        [JsonProperty("heading")]
        public string Heading { get; set; }

        [JsonProperty("introduction")]
        public string Introduction { get; set; }

        [JsonProperty("image")]
        public Item Image { get; set; }

        [JsonProperty("stepperLinks")]
        public List<ResponseHubSectionLink> StepperLinks { get; set; }

        [JsonProperty("standardLinks")]
        public List<ResponseHubSectionLink> StandardLinks { get; set; }

        [JsonProperty("statisticsSections")]
        public List<ResponseHubStatistic> StatisticsSections { get; set; }

        [JsonProperty("ctaPanel")]
        public ResponseCtaPanel CtaPanel { get; set; }
    }

    public class ResponseHubStatistic
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("highlightValue")]
        public string HighlightValue { get; set; }

        [JsonProperty("quoteName")]
        public string QuoteName { get; set; }

        [JsonProperty("quoteRole")]
        public string QuoteRole { get; set; }

        [JsonProperty("referenceText")]
        public string ReferenceText { get; set; }
    }

    public class ResponseHubSectionLink
    {
        public string Id { get; set; }
        public int? PageType { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string MetaDescription { get; set; }

        [JsonProperty("landingPage")]
        public CardLandingPageResponse LandingPage { get; set; }

        [JsonProperty("ctaPanel")]
        public ResponseCtaPanel CtaPanel { get; set; }
    }

    public class ResponseCtaPanel
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ButtonText { get; set; }
        public string Url { get; set; }
    }
}
