using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetAdvertsResponse
    {
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("totalFound")]
        public long TotalFound { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("vacancies")]
        public List<Vacancy> Vacancies { get; set; }
    }
    public class Vacancy
    {
        [JsonProperty("location")]
        public VacancyLocation Location { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("closingDate")]
        public DateTime ClosingDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("distance")]
        public double? Distance { get; set; }

        [JsonProperty("postedDate")]
        public DateTime PostedDate { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("employerName")]
        public string EmployerName { get; set; }

        [JsonProperty("vacancyReference")]
        public string VacancyReference { get; set; }
    }

    public class VacancyLocation
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

    public class Route
    {
        [JsonProperty("route")]
        public string Name { get; set; }
    }
    
    public class Location
    {
        [JsonProperty("geoPoint")]
        public List<double> GeoPoint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}