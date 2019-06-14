// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace VacanciesApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TraineeshipVacancy
    {
        /// <summary>
        /// Initializes a new instance of the TraineeshipVacancy class.
        /// </summary>
        public TraineeshipVacancy()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TraineeshipVacancy class.
        /// </summary>
        public TraineeshipVacancy(int? vacancyReference = default(int?), string title = default(string), string shortDescription = default(string), string description = default(string), string workingWeek = default(string), string expectedDuration = default(string), System.DateTime? expectedStartDate = default(System.DateTime?), System.DateTime? postedDate = default(System.DateTime?), System.DateTime? applicationClosingDate = default(System.DateTime?), int? numberOfPositions = default(int?), string traineeshipSector = default(string), string employerName = default(string), string employerDescription = default(string), string employerWebsite = default(string), string trainingToBeProvided = default(string), string qualificationsRequired = default(string), string skillsRequired = default(string), string personalQualities = default(string), string futureProspects = default(string), string thingsToConsider = default(string), bool? isNationwide = default(bool?), string supplementaryQuestion1 = default(string), string supplementaryQuestion2 = default(string), string vacancyUrl = default(string), GeoCodedAddress location = default(GeoCodedAddress), string contactName = default(string), string contactEmail = default(string), string contactNumber = default(string), string trainingProviderName = default(string), string trainingProviderUkprn = default(string), string trainingProviderSite = default(string), bool? isEmployerDisabilityConfident = default(bool?), string applicationInstructions = default(string), string applicationUrl = default(string))
        {
            VacancyReference = vacancyReference;
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            WorkingWeek = workingWeek;
            ExpectedDuration = expectedDuration;
            ExpectedStartDate = expectedStartDate;
            PostedDate = postedDate;
            ApplicationClosingDate = applicationClosingDate;
            NumberOfPositions = numberOfPositions;
            TraineeshipSector = traineeshipSector;
            EmployerName = employerName;
            EmployerDescription = employerDescription;
            EmployerWebsite = employerWebsite;
            TrainingToBeProvided = trainingToBeProvided;
            QualificationsRequired = qualificationsRequired;
            SkillsRequired = skillsRequired;
            PersonalQualities = personalQualities;
            FutureProspects = futureProspects;
            ThingsToConsider = thingsToConsider;
            IsNationwide = isNationwide;
            SupplementaryQuestion1 = supplementaryQuestion1;
            SupplementaryQuestion2 = supplementaryQuestion2;
            VacancyUrl = vacancyUrl;
            Location = location;
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactNumber = contactNumber;
            TrainingProviderName = trainingProviderName;
            TrainingProviderUkprn = trainingProviderUkprn;
            TrainingProviderSite = trainingProviderSite;
            IsEmployerDisabilityConfident = isEmployerDisabilityConfident;
            ApplicationInstructions = applicationInstructions;
            ApplicationUrl = applicationUrl;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "vacancyReference")]
        public int? VacancyReference { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "shortDescription")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "workingWeek")]
        public string WorkingWeek { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "expectedDuration")]
        public string ExpectedDuration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "expectedStartDate")]
        public System.DateTime? ExpectedStartDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "postedDate")]
        public System.DateTime? PostedDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationClosingDate")]
        public System.DateTime? ApplicationClosingDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "numberOfPositions")]
        public int? NumberOfPositions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "traineeshipSector")]
        public string TraineeshipSector { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "employerName")]
        public string EmployerName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "employerDescription")]
        public string EmployerDescription { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "employerWebsite")]
        public string EmployerWebsite { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingToBeProvided")]
        public string TrainingToBeProvided { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "qualificationsRequired")]
        public string QualificationsRequired { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "skillsRequired")]
        public string SkillsRequired { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "personalQualities")]
        public string PersonalQualities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "futureProspects")]
        public string FutureProspects { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "thingsToConsider")]
        public string ThingsToConsider { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isNationwide")]
        public bool? IsNationwide { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "supplementaryQuestion1")]
        public string SupplementaryQuestion1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "supplementaryQuestion2")]
        public string SupplementaryQuestion2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "vacancyUrl")]
        public string VacancyUrl { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public GeoCodedAddress Location { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contactName")]
        public string ContactName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contactEmail")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contactNumber")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingProviderName")]
        public string TrainingProviderName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingProviderUkprn")]
        public string TrainingProviderUkprn { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingProviderSite")]
        public string TrainingProviderSite { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isEmployerDisabilityConfident")]
        public bool? IsEmployerDisabilityConfident { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationInstructions")]
        public string ApplicationInstructions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "applicationUrl")]
        public string ApplicationUrl { get; set; }

    }
}