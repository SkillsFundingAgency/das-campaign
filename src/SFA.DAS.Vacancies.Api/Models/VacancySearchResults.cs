using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.DAS.Vacancies.Api.Models
{
    public partial class VacancySearchResults
    {
        [JsonProperty("totalMatched")]
        public long TotalMatched { get; set; }

        [JsonProperty("totalReturned")]
        public long TotalReturned { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("totalPages")]
        public double TotalPages { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("vacancyReference")]
        public long VacancyReference { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("expectedStartDate")]
        public DateTimeOffset ExpectedStartDate { get; set; }

        [JsonProperty("postedDate")]
        public DateTimeOffset PostedDate { get; set; }

        [JsonProperty("applicationClosingDate")]
        public DateTimeOffset ApplicationClosingDate { get; set; }

        [JsonProperty("numberOfPositions")]
        public long NumberOfPositions { get; set; }

        [JsonProperty("trainingType")]
        public TrainingType TrainingType { get; set; }

        [JsonProperty("trainingTitle")]
        public string TrainingTitle { get; set; }

        [JsonProperty("trainingCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainingCode { get; set; }

        [JsonProperty("employerName")]
        public string EmployerName { get; set; }

        [JsonProperty("trainingProviderName")]
        public string TrainingProviderName { get; set; }

        [JsonProperty("isNationwide")]
        public bool IsNationwide { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("apprenticeshipLevel")]
        public ApprenticeshipLevel ApprenticeshipLevel { get; set; }

        [JsonProperty("vacancyUrl")]
        public Uri VacancyUrl { get; set; }

        [JsonProperty("isEmployerDisabilityConfident")]
        public bool IsEmployerDisabilityConfident { get; set; }

        [JsonProperty("distanceInMiles")]
        public double DistanceInMiles { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }

    public enum ApprenticeshipLevel { Advanced,
        Intermediate,
        Higher,
        Degree,
        FoundationDegree,
        MastersDegree
    };

    public enum TrainingType { Framework, Standard };

    public partial class VacancySearchResults
    {
        public static VacancySearchResults FromJson(string json) => JsonConvert.DeserializeObject<VacancySearchResults>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this VacancySearchResults self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                ApprenticeshipLevelConverter.Singleton,
                TrainingTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ApprenticeshipLevelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ApprenticeshipLevel) || t == typeof(ApprenticeshipLevel?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Advanced":
                    return ApprenticeshipLevel.Advanced;
                case "Intermediate":
                    return ApprenticeshipLevel.Intermediate;
                case "Higher":
                    return ApprenticeshipLevel.Higher;
                case "Degree":
                    return ApprenticeshipLevel.Degree;
                case "Foundation":
                    return ApprenticeshipLevel.FoundationDegree;
                case "Masters":
                    return ApprenticeshipLevel.MastersDegree;
            }
            throw new Exception("Cannot unmarshal type ApprenticeshipLevel");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ApprenticeshipLevel)untypedValue;
            switch (value)
            {
                case ApprenticeshipLevel.Advanced:
                    serializer.Serialize(writer, "Advanced");
                    return;
                case ApprenticeshipLevel.Intermediate:
                    serializer.Serialize(writer, "Intermediate");
                    return;
            }
            throw new Exception("Cannot marshal type ApprenticeshipLevel");
        }

        public static readonly ApprenticeshipLevelConverter Singleton = new ApprenticeshipLevelConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class TrainingTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TrainingType) || t == typeof(TrainingType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Framework":
                    return TrainingType.Framework;
                case "Standard":
                    return TrainingType.Standard;
            }
            throw new Exception("Cannot unmarshal type TrainingType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TrainingType)untypedValue;
            switch (value)
            {
                case TrainingType.Framework:
                    serializer.Serialize(writer, "Framework");
                    return;
                case TrainingType.Standard:
                    serializer.Serialize(writer, "Standard");
                    return;
            }
            throw new Exception("Cannot marshal type TrainingType");
        }

        public static readonly TrainingTypeConverter Singleton = new TrainingTypeConverter();
    }
}
