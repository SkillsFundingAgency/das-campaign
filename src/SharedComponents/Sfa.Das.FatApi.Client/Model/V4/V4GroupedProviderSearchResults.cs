using Newtonsoft.Json;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4GroupedProviderSearchResults
    {
        [JsonProperty("HasNationalProviders")]
        public bool HasNationalProviders { get; set; }

        [JsonProperty("TotalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("PageSize")]
        public long PageSize { get; set; }

        [JsonProperty("PageNumber")]
        public long PageNumber { get; set; }

        [JsonProperty("Results")]
        public V4GroupedProviderSearchResultItem[] Results { get; set; }
    }
}
