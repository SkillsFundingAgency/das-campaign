using Newtonsoft.Json;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4Address
    {
        [JsonProperty("Address1", NullValueHandling = NullValueHandling.Ignore)]
        public string Address1 { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("Town", NullValueHandling = NullValueHandling.Ignore)]
        public string Town { get; set; }

        [JsonProperty("PostCode")]
        public string PostCode { get; set; }

        [JsonProperty("County", NullValueHandling = NullValueHandling.Ignore)]
        public string County { get; set; }
    }
}
