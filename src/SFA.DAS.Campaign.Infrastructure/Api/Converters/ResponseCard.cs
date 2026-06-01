using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseCard
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string HubType { get; set; }
        public string Summary { get; set; }

        [JsonProperty("cardImage")]
        public Item CardImage { get; set; }

        public CardLandingPageResponse LandingPage { get; set; }
    }

    public class CardLandingPageResponse
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Hub { get; set; }
    }
}