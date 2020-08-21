using Newtonsoft.Json;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4ClosestLocationsSearchResults
    {
        [JsonProperty("TotalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("PageSize")]
        public long PageSize { get; set; }

        [JsonProperty("PageNumber")]
        public long PageNumber { get; set; }

        [JsonProperty("ProviderName")]
        public string ProviderName { get; set; }

        [JsonProperty("Results")]
        public V4ClosestLocationsSearchResultItem[] Results { get; set; }
    }
}
