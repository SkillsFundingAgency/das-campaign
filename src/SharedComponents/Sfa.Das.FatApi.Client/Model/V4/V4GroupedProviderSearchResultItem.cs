using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sfa.Das.FatApi.Client.Model.V4
{
    public partial class V4GroupedProviderSearchResultItem
    {
        [JsonProperty("HasOtherMatchingLocations")]
        public bool HasOtherMatchingLocations { get; set; }

        [JsonProperty("Ukprn")]
        public int Ukprn { get; set; }

        [JsonProperty("Location")]
        public V4Location Location { get; set; }

        [JsonProperty("ProviderName")]
        public string ProviderName { get; set; }

        [JsonProperty("NationalProvider")]
        public bool NationalProvider { get; set; }

        [JsonProperty("DeliveryModes")]
        public List<string> DeliveryModes { get; set; }

        [JsonProperty("Distance")]
        public double Distance { get; set; }

        [JsonProperty("NationalOverallAchievementRate")]
        public double NationalOverallAchievementRate { get; set; }

        [JsonProperty("HasNonLevyContract")]
        public bool HasNonLevyContract { get; set; }

        [JsonProperty("IsLevyPayerOnly")]
        public bool IsLevyPayerOnly { get; set; }

        [JsonProperty("CurrentlyNotStartingNewApprentices")]
        public bool CurrentlyNotStartingNewApprentices { get; set; }

        [JsonProperty("IsHigherEducationInstitute")]
        public bool IsHigherEducationInstitute { get; set; }

        [JsonProperty("OverallAchievementRate", NullValueHandling = NullValueHandling.Ignore)]
        public double? OverallAchievementRate { get; set; }

        [JsonProperty("EmployerSatisfaction", NullValueHandling = NullValueHandling.Ignore)]
        public double? EmployerSatisfaction { get; set; }

        [JsonProperty("LearnerSatisfaction", NullValueHandling = NullValueHandling.Ignore)]
        public double? LearnerSatisfaction { get; set; }

        [JsonProperty("OverallCohort", NullValueHandling = NullValueHandling.Ignore)]
        public string OverallCohort { get; set; }
    }
}
