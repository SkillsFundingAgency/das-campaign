using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public interface IApprenticeshipProviderSearchResultsItem
    {
        string ContactUsUrl { get; set; }
        List<string> DeliveryModes { get; set; }
        List<string> DeliveryModesKeywords { get; set; }
        double Distance { get; set; }
        string Email { get; set; }
        bool NationalProvider { get; set; }
        double? EmployerSatisfaction { get; set; }
        double? LearnerSatisfaction { get; set; }
        double? OverallAchievementRate { get; set; }
        string MarketingName { get; set; }
        string ProviderMarketingInfo { get; set; }
        string ApprenticeshipMarketingInfo { get; set; }
        string ProviderName { get; set; }
        string LegalName { get; set; }
        string Phone { get; set; }
        string ApprenticeshipInfoUrl { get; set; }
        int Ukprn { get; set; }
        bool IsHigherEducationInstitute { get; set; }
        string Website { get; set; }
        IEnumerable<TrainingLocation> TrainingLocations { get; set; }
        int? MatchingLocationId { get; set; }

        double? NationalOverallAchievementRate { get; set; }

        string OverallCohort { get; set; }

        bool HasNonLevyContract { get; set; }

        bool HasParentCompanyGuarantee { get; set; }

        bool IsNew { get; set; }

		bool CurrentlyNotStartingNewApprentices { get; set; }

        Feedback ProviderFeedback { get; set; }
    }
}