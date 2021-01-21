using Newtonsoft.Json;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4Location
    {
        [JsonProperty("LocationId")]
        public int LocationId { get; set; }

        [JsonProperty("LocationName")]
        public string LocationName { get; set; }

        [JsonProperty("Address")]
        public V4Address Address { get; set; }
    }
}
