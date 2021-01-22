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
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    
}