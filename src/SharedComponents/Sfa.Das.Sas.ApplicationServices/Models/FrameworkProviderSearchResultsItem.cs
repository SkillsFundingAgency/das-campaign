using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class FrameworkProviderSearchResultsItem : IApprenticeshipProviderSearchResultsItem
    {
        public int Ukprn { get; set; }

        public bool IsHigherEducationInstitute { get; set; }

        public string ProviderName { get; set; }

        public string LegalName { get; set; }

        public string FrameworkId { get; set; }

        public int FrameworkCode { get; set; }

        public int PathwayCode { get; set; }

        public int Level { get; set; }

        public double? OverallAchievementRate { get; set; }

        public string MarketingName { get; set; }

        public string ProviderMarketingInfo { get; set; }

        public string ApprenticeshipMarketingInfo { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool NationalProvider { get; set; }

        public string ContactUsUrl { get; set; }

        public string ApprenticeshipInfoUrl { get; set; }

        public List<string> DeliveryModes { get; set; }

        public List<string> DeliveryModesKeywords { get; set; }

        public string Website { get; set; }

        public IEnumerable<TrainingLocation> TrainingLocations { get; set; }

        public double Distance { get; set; }

        public double? EmployerSatisfaction { get; set; }

        public double? LearnerSatisfaction { get; set; }

        public int? MatchingLocationId { get; set; }

        public double? NationalOverallAchievementRate { get; set; }

        public string OverallCohort { get; set; }

        public bool HasNonLevyContract { get; set; }

        public bool HasParentCompanyGuarantee { get; set; }

        public bool IsNew { get; set; }

        public bool CurrentlyNotStartingNewApprentices { get; set; }

        public Feedback ProviderFeedback { get; set; }
    }
}