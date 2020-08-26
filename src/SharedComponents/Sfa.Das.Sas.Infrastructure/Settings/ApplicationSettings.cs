using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Infrastructure.Settings
{
    public sealed class ApplicationSettings : IConfigurationSettings, IPostcodeIOConfigurationSettings
    {
        public string ApprenticeshipIndexAlias => ConfigurationManager.AppSettings["ApprenticeshipIndexAlias"];

        public string ProviderIndexAlias => ConfigurationManager.AppSettings["ProviderIndexAlias"];
        public string ApprenticeshipApiBaseUrl => ConfigurationManager.AppSettings["ApprenticeshipApiBaseUrl"];

        public string BuildId => ConfigurationManager.AppSettings["BuildId"];

        public Uri SurveyUrl => new Uri(ConfigurationManager.AppSettings["SurveyUrl"]);

        public string CookieInfoBannerCookieName => ConfigurationManager.AppSettings["CookieInfoBannerCookieName"];

        public Uri PostcodeUrl => new Uri(ConfigurationManager.AppSettings["PostcodeUrl"]);

        public Uri PostcodeTerminatedUrl => new Uri(ConfigurationManager.AppSettings["PostcodeTerminatedUrl"]);

        public string EnvironmentName => ConfigurationManager.AppSettings["EnvironmentName"];

        public Uri SatisfactionSourceUrl => new Uri(ConfigurationManager.AppSettings["SatisfactionSourceUrl"]);

        public Uri AchievementRateUrl => new Uri(ConfigurationManager.AppSettings["AchievementRateUrl"]);

        public Uri CookieImprovementUrl => new Uri(ConfigurationManager.AppSettings["CookieImprovementUrl"]);

        public Uri CookieGoogleUrl => new Uri(ConfigurationManager.AppSettings["CookieGoogleUrl"]);

        public Uri CookieApplicationInsightsUrl => new Uri(ConfigurationManager.AppSettings["CookieApplicationInsightsUrl"]);

        public Uri CookieAboutUrl => new Uri(ConfigurationManager.AppSettings["CookieAboutUrl"]);

        public Uri SurveyProviderUrl => new Uri(ConfigurationManager.AppSettings["SurveyProviderUrl"]);

        public Uri ManageApprenticeshipFundsUrl => new Uri(ConfigurationManager.AppSettings["ManageApprenticeshipFundsUrl"]);

        public IEnumerable<long> HideAboutProviderForUkprns => GetHideAboutProviderUrkprns();

        private IEnumerable<long> GetHideAboutProviderUrkprns()
        {
            return
                ConfigurationManager.AppSettings["HideAboutProviderForUkprns"]
                    .Split(',')
                    .Select(m => m.Trim())
                    .Where(m => System.Text.RegularExpressions.Regex.IsMatch(m, "^[0-9]{1,18}$"))
                    .Where(m => !string.IsNullOrEmpty(m))
                    .Select(m => long.Parse(m));
        }
    }
}