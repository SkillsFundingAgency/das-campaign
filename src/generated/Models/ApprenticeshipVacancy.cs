// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace VacanciesApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ApprenticeshipVacancy
    {
        /// <summary>
        /// Initializes a new instance of the ApprenticeshipVacancy class.
        /// </summary>
        public ApprenticeshipVacancy()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ApprenticeshipVacancy class.
        /// </summary>
        /// <param name="wageUnit">Possible values include: 'Unspecified',
        /// 'Weekly', 'Monthly', 'Annually'</param>
        /// <param name="trainingType">Possible values include: 'Unavailable',
        /// 'Framework', 'Standard'</param>
        public ApprenticeshipVacancy(long? vacancyReference = default(long?), string title = default(string), string shortDescription = default(string), string description = default(string), string wageUnit = default(string), string workingWeek = default(string), string wageText = default(string), string wageAdditionalInformation = default(string), double? hoursPerWeek = default(double?), string expectedDuration = default(string), System.DateTime? expectedStartDate = default(System.DateTime?), System.DateTime? postedDate = default(System.DateTime?), System.DateTime? applicationClosingDate = default(System.DateTime?), int? numberOfPositions = default(int?), string trainingType = default(string), string trainingTitle = default(string), string trainingCode = default(string), string employerName = default(string), string employerDescription = default(string), string employerWebsite = default(string), string trainingToBeProvided = default(string), string qualificationsRequired = default(string), string skillsRequired = default(string), string personalQualities = default(string), string futureProspects = default(string), string thingsToConsider = default(string), bool? isNationwide = default(bool?), string supplementaryQuestion1 = default(string), string supplementaryQuestion2 = default(string), string vacancyUrl = default(string), GeoCodedAddress location = default(GeoCodedAddress), string contactName = default(string), string contactEmail = default(string), string contactNumber = default(string), string trainingProviderName = default(string), string trainingProviderUkprn = default(string), string trainingProviderSite = default(string), bool? isEmployerDisabilityConfident = default(bool?), string apprenticeshipLevel = default(string), string applicationInstructions = default(string), string applicationUrl = default(string))
        {
            VacancyReference = vacancyReference;
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            WageUnit = wageUnit;
            WorkingWeek = workingWeek;
            WageText = wageText;
            WageAdditionalInformation = wageAdditionalInformation;
            HoursPerWeek = hoursPerWeek;
            ExpectedDuration = expectedDuration;
            ExpectedStartDate = expectedStartDate;
            PostedDate = postedDate;
            ApplicationClosingDate = applicationClosingDate;
            NumberOfPositions = numberOfPositions;
            TrainingType = trainingType;
            TrainingTitle = trainingTitle;
            TrainingCode = trainingCode;
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
            ApprenticeshipLevel = apprenticeshipLevel;
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
        public long? VacancyReference { get; set; }

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
        /// Gets or sets possible values include: 'Unspecified', 'Weekly',
        /// 'Monthly', 'Annually'
        /// </summary>
        [JsonProperty(PropertyName = "wageUnit")]
        public string WageUnit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "workingWeek")]
        public string WorkingWeek { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "wageText")]
        public string WageText { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "wageAdditionalInformation")]
        public string WageAdditionalInformation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hoursPerWeek")]
        public double? HoursPerWeek { get; set; }

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
        /// Gets or sets possible values include: 'Unavailable', 'Framework',
        /// 'Standard'
        /// </summary>
        [JsonProperty(PropertyName = "trainingType")]
        public string TrainingType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingTitle")]
        public string TrainingTitle { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "trainingCode")]
        public string TrainingCode { get; set; }

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
        [JsonProperty(PropertyName = "apprenticeshipLevel")]
        public string ApprenticeshipLevel { get; set; }

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