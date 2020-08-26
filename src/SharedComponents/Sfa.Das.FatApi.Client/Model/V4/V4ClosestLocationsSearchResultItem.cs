using Newtonsoft.Json;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4ClosestLocationsSearchResultItem
    {
        [JsonProperty("Distance")]
        public double Distance { get; set; }

        [JsonProperty("Location")]
        public V4Location Location { get; set; }
    }
}
