using System.Collections.Generic;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class TrainingProviderDetailsViewModel
    {
        public TrainingProviderDetailQueryViewModel SearchQuery { get; set; }
        public ApprenticeshipDetailBase Apprenticeship { get; set; }
        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public Location Location { get; set; }

        public string MarketingInfo { get; set; }

        public List<string> DeliveryModes { get; set; }

        public ContactInformation ContactInformation { get; set; }

        public Address Address { get; set; }

        public int? EmployerSatisfaction { get; set; }

        public string EmployerSatisfactionMessage { get; set; }

        public double? LearnerSatisfaction { get; set; }

        public string LearnerSatisfactionMessage { get; set; }

        public string ApprenticeshipName { get; set; }

        public string ApprenticeshipLevel { get; set; }

        public string SurveyUrl { get; set; }

        public string NationalAchievementRateMessage { get; set; }

        public bool NationalProvider { get; set; }

        public string AchievementRateMessage { get; set; }

        public string OverallCohort { get; set; }

        public int AchievementRate { get; set; }

        public int NationalAchievementRate { get; set; }

        public string SatisfactionSourceUrl { get; set; }

        public string AchievementRateSourceUrl { get; set; }

        public string LocationAddressLine { get; set; }

        public bool IsLevyPayingEmployer { get; set; }

        public bool HasParentCompanyGuarantee { get; set; }

        public bool IsNewProvider { get; set; }

        public bool HasNonLevyContract { get; set; }

        public bool IsLevyPayerOnly { get; set;}

        public bool CurrentlyNotStartingNewApprentices { get; set; }

        public bool RegulatedApprenticeship { get; set; }

        public string ApprenticeshipId { get; set; }

        public string Postcode { get; internal set; }

        public FeedbackViewModel Feedback { get; set; }

        public string AboutApprenticeshipInfo { get; set; }
    }
}