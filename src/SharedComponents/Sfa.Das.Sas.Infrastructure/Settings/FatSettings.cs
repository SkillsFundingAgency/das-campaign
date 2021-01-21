
using System.Configuration;

namespace Sfa.Das.Sas.Infrastructure.Settings
{

    using Sfa.Das.Sas.Core.Configuration;

    public sealed class FatSettings : IFatConfigurationSettings
    {
        public string FatApiBaseUrl => ConfigurationManager.AppSettings["ApprenticeshipApiBaseUrl"];
        public string SaveEmployerFavouritesUrl => throw new System.NotImplementedException();
    }
}