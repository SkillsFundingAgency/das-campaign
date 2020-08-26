﻿namespace Sfa.Das.Sas.Infrastructure.Repositories
{
    using System;
    using ApplicationServices.Http;
    using Core.Configuration;
    using Core.Domain.Model;
    using Core.Domain.Repositories;
    using Newtonsoft.Json;
    using Sfa.Das.Sas.Core;
    using SFA.DAS.NLog.Logger;

    public sealed class ApprenticeshipProviderApiRepository : IApprenticeshipProviderRepository
    {
        private readonly ILog _applicationLogger;
        private readonly IFatConfigurationSettings _fatSettings;
        private readonly IHttpGet _httpService;

        public ApprenticeshipProviderApiRepository(ILog applicationLogger,
            IFatConfigurationSettings fatSettings,
            IHttpGet httpService)
        {
            _applicationLogger = applicationLogger;
            _fatSettings = fatSettings;
            _httpService = httpService;
        }

        public ApprenticeshipDetails GetCourseByStandardCode(int ukprn, int locationId, string standardCode)
        {
            var url = string.Format(
                "{0}standards/{1}/providers?ukprn={2}&location={3}",
                _fatSettings.FatApiBaseUrl.AddSlash(),
                standardCode,
                ukprn,
                locationId);

            var result = JsonConvert.DeserializeObject<ApprenticeshipDetails>(_httpService.Get(url, null, null));

            if (result == null)
            {
                throw new ApplicationException($"Failed to get framework with id {standardCode}");
            }

            return result;
        }

        public ApprenticeshipDetails GetCourseByFrameworkId(int ukprn, int locationId, string frameworkId)
        {
            var url = string.Format(
                "{0}frameworks/{1}/providers?ukprn={2}&location={3}",
                _fatSettings.FatApiBaseUrl.AddSlash(),
                frameworkId,
                ukprn,
                locationId);

            var requestResponse = _httpService.Get(url, null, null);

            if (requestResponse == null)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<ApprenticeshipDetails>(requestResponse);

            if (result == null)
            {
                throw new ApplicationException($"Failed to get framework with id {frameworkId}");
            }

            return result;
        }

        public int GetFrameworksAmountWithProviders()
        {
           throw new NotImplementedException();
        }

        public int GetStandardsAmountWithProviders()
        {
           throw new NotImplementedException();
        }
    }
}