using Sfa.Das.Sas.Core.Configuration;
using System;
using System.Web;

namespace Sfa.Das.Sas.Shared.Components.Configuration
{
    internal class SaveApprenticeshipUrlBuilder
    {
        private readonly IFatConfigurationSettings _config;

        public SaveApprenticeshipUrlBuilder(IFatConfigurationSettings config)
        {
            _config = config;
        }

        public Uri GenerateSaveUrl(string basketId)
        {
            var saveUrl = new Uri(_config.SaveEmployerFavouritesUrl);
            var builder = new UriBuilder(saveUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["basketId"] = basketId;

            builder.Query = query.ToString();

            return builder.Uri;
        }
    }

}
