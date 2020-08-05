using System;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public class ApprenticeshipOrchestrator : IApprenticeshipOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly ILog _logger;
        private readonly IFrameworkDetailsViewModelMapper _frameworkDetailsViewModelMapper;
        private readonly IStandardDetailsViewModelMapper _standardDetailsViewModelMapper;
        private readonly ICacheStorageService _cacheService;
        private readonly ICacheSettings _cacheSettings;

        public ApprenticeshipOrchestrator(IMediator mediator, ILog logger, IFrameworkDetailsViewModelMapper frameworkDetailsViewModelMapper, IStandardDetailsViewModelMapper standardDetailsViewModelMapper, ICacheStorageService cacheStorageService, ICacheSettings cacheSettings)
        {
            _mediator = mediator;
            _logger = logger;
            _frameworkDetailsViewModelMapper = frameworkDetailsViewModelMapper;
            _standardDetailsViewModelMapper = standardDetailsViewModelMapper;
            _cacheService = cacheStorageService;
            _cacheSettings = cacheSettings;
        }

        public async Task<FrameworkDetailsViewModel> GetFramework(string id)
        {
            var cacheKey = $"FatComponentsCache-Apprenticeship_details-{id}";

            var cacheEntry = await _cacheService.RetrieveFromCache<FrameworkDetailsViewModel>(cacheKey);

            if (cacheEntry == null)
            {
                var response = await _mediator.Send(new GetFrameworkQuery { Id = id });

                string message;

                switch (response.StatusCode)
                {
                    case GetFrameworkResponse.ResponseCodes.InvalidFrameworkId:
                        _logger.Info("404 - Framework id has wrong format");
                        throw new ArgumentException($"Framework id: {id} has wrong format");

                    case GetFrameworkResponse.ResponseCodes.FrameworkNotFound:
                        message = $"Cannot find framework: {id}";
                        _logger.Warn($"404 - {message}");

                        throw new ArgumentException(message);

                    case GetFrameworkResponse.ResponseCodes.Gone:
                        message = $"Expired framework request: {id}";

                        _logger.Warn($"410 - {message}");

                        throw new ArgumentException(message);

                    case GetFrameworkResponse.ResponseCodes.Success:
                        _logger.Info($"mapping framework {id}");
                        cacheEntry = _frameworkDetailsViewModelMapper.Map(response.Framework);

                        _logger.Info($"Saving to cache");
                        await _cacheService.SaveToCache(cacheKey, cacheEntry, new TimeSpan(_cacheSettings.CacheAbsoluteExpirationDays, 0, 0, 0), new TimeSpan(_cacheSettings.CacheSlidingExpirationDays, 0, 0, 0));

                        break;

                    default:
                        _logger.Info($"Cannot handle GetFrameworkQuery response: {response.StatusCode.ToString()}");
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            
            return cacheEntry;
        }

        public async Task<StandardDetailsViewModel> GetStandard(string id)
        {
            _logger.Info($"Checking cache from standard {id}");

            var cacheKey = $"FatComponentsCache-Apprenticeship_details-{id}";

            var cacheEntry = await _cacheService.RetrieveFromCache<StandardDetailsViewModel>(cacheKey);

            if (cacheEntry == null)
            {
                _logger.Info($"Getting standard {id}");
                var response = await _mediator.Send(new GetStandardQuery { Id = id });

                string message;

                switch (response.StatusCode)
                {
                    case GetStandardResponse.ResponseCodes.InvalidStandardId:
                        {
                            _logger.Info("404 - Attempt to get standard with an ID below zero");
                            throw new Exception("Cannot find any standards with an ID below zero");
                        }

                    case GetStandardResponse.ResponseCodes.StandardNotFound:
                        {
                            message = $"Cannot find standard: {id}";
                            _logger.Warn($"404 - {message}");

                            throw new Exception(message);
                        }

                    case GetStandardResponse.ResponseCodes.AssessmentOrgsEntityNotFound:
                        {
                            message = $"Cannot find assessment organisations for standard: {id}";
                            _logger.Warn($"404 - {message}");
                            break;
                        }

                    case GetStandardResponse.ResponseCodes.Gone:
                        {
                            message = $"Expired standard request: {id}";

                            _logger.Warn($"410 - {message}");

                            throw new Exception(message);
                        }

                    case GetStandardResponse.ResponseCodes.HttpRequestException:
                        {
                            message = $"Request error when requesting assessment orgs for standard: {id}";
                            _logger.Warn($"400 - {message}");

                            throw new Exception(message);
                        }
                
                }

                _logger.Info($"Mapping Standard {id}");
                cacheEntry = _standardDetailsViewModelMapper.Map(response.Standard, response.AssessmentOrganisations);

                _logger.Info($"Saving to cache");
                await _cacheService.SaveToCache(cacheKey, cacheEntry,  new TimeSpan(_cacheSettings.CacheAbsoluteExpirationDays, 0, 0, 0), new TimeSpan(_cacheSettings.CacheSlidingExpirationDays, 0, 0, 0));
            }
            
            return cacheEntry;
        }

        public ApprenticeshipType GetApprenticeshipType(string id)
        {
            return id.Contains("-") ? ApprenticeshipType.Framework : ApprenticeshipType.Standard;
        }


    }
}
