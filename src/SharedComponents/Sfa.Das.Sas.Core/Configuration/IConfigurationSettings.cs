using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Core.Configuration
{
    public interface IConfigurationSettings
    {
        string ApprenticeshipIndexAlias { get; }

        string ProviderIndexAlias { get; }
        string ApprenticeshipApiBaseUrl { get; }
        string BuildId { get; }

        Uri SurveyUrl { get; }

        string CookieInfoBannerCookieName { get; }

        Uri PostcodeUrl { get; }

        Uri PostcodeTerminatedUrl { get; }

        string EnvironmentName { get; }

        Uri SatisfactionSourceUrl { get; }

        Uri CookieImprovementUrl { get; }

        Uri CookieGoogleUrl { get; }

        Uri CookieApplicationInsightsUrl { get; }

        Uri CookieAboutUrl { get; }

        Uri SurveyProviderUrl { get; }

        Uri AchievementRateUrl { get; }

        IEnumerable<long> HideAboutProviderForUkprns { get; }

        Uri ManageApprenticeshipFundsUrl
        {
            get;
        }
    }
}