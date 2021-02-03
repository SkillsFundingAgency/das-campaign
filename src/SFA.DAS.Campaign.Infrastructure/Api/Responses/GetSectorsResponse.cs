using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetSectorsResponse
    {
        [JsonProperty("sectors")]
        public List<Sector> Sectors { get; set; }
    }
    public class Sector
    {
        [JsonProperty("route")]
        public string Route { get; set; }
    }
}