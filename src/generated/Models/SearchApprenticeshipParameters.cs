// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace VacanciesApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SearchApprenticeshipParameters
    {
        /// <summary>
        /// Initializes a new instance of the SearchApprenticeshipParameters
        /// class.
        /// </summary>
        public SearchApprenticeshipParameters()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SearchApprenticeshipParameters
        /// class.
        /// </summary>
        public SearchApprenticeshipParameters(string standardLarsCodes = default(string), string frameworkLarsCodes = default(string), int? pageSize = default(int?), int? pageNumber = default(int?), int? postedInLastNumberOfDays = default(int?), bool? nationwideOnly = default(bool?), double? latitude = default(double?), double? longitude = default(double?), int? distanceInMiles = default(int?), string sortBy = default(string))
        {
            StandardLarsCodes = standardLarsCodes;
            FrameworkLarsCodes = frameworkLarsCodes;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PostedInLastNumberOfDays = postedInLastNumberOfDays;
            NationwideOnly = nationwideOnly;
            Latitude = latitude;
            Longitude = longitude;
            DistanceInMiles = distanceInMiles;
            SortBy = sortBy;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "standardLarsCodes")]
        public string StandardLarsCodes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "frameworkLarsCodes")]
        public string FrameworkLarsCodes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pageNumber")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "postedInLastNumberOfDays")]
        public int? PostedInLastNumberOfDays { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nationwideOnly")]
        public bool? NationwideOnly { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "distanceInMiles")]
        public int? DistanceInMiles { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sortBy")]
        public string SortBy { get; set; }

    }
}