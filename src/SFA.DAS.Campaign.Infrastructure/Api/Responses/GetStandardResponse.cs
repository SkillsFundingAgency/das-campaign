using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetStandardResponse
    {
        [JsonProperty("larsCode")]
        public int LarsCode { get; set; }
        [JsonProperty("standardUId")]
        public string StandardUId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("maxFunding")]
        public int MaxFunding { get; set; }
    }
}
