using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetStandardsResponse
    {
        [JsonProperty("standards")]
        public List<Standard> Standards { get; set; }
    }

    public class Standard
    {
        [JsonProperty("larsCode")]
        public int LarsCode { get; set; }
        [JsonProperty("standarduid")]
        public string StandardUId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
    }

    
}