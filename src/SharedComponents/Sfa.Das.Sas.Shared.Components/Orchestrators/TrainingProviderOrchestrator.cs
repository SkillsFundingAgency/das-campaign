using System;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.ApplicationServices.Queries;
using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public class TrainingProviderOrchestrator : ITrainingProviderOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly ISearchResultsViewModelMapper _searchResultsViewModelMapper;
        private readonly ITrainingProviderDetailsViewModelMapper _trainingProviderDetailsViewModelMapper;
        private readonly ITrainingProviderSearchFilterViewModelMapper _trainingProviderSearchFilterViewModelMapper;
        private readonly ITrainingProviderClosestLocationsViewModelMapper _trainingProviderClosestLocationsViewModelMapper;
        private readonly ILog _logger;
        private readonly ICacheStorageService _cacheService;
        private readonly ICacheSettings _cacheSettings;

        public TrainingProviderOrchestrator(
            IMediator mediator,
            ISearchResultsViewModelMapper searchResultsViewModelMapper,
            ILog logger,
            ITrainingProviderDetailsViewModelMapper trainingProviderDetailsViewModelMapper,
            ITrainingProviderSearchFilterViewModelMapper trainingProviderSearchFilterViewModelMapper,
            ICacheStorageService cacheService,
            ICacheSettings cacheSettings,
            ITrainingProviderClosestLocationsViewModelMapper trainingProviderClosestLocationsViewModelMapper)
        {
            _mediator = mediator;
            _searchResultsViewModelMapper = searchResultsViewModelMapper;
            _logger = logger;
            _trainingProviderDetailsViewModelMapper = trainingProviderDetailsViewModelMapper;
            _trainingProviderSearchFilterViewModelMapper = trainingProviderSearchFilterViewModelMapper;
            _cacheService = cacheService;
            _cacheSettings = cacheSettings;
            _trainingProviderClosestLocationsViewModelMapper = trainingProviderClosestLocationsViewModelMapper;
        }

        public async Task<SearchResultsViewModel<TrainingProviderSearchResultsItem, TrainingProviderSearchViewModel>> GetSearchResults(TrainingProviderSearchViewModel searchQueryModel)
        {
            var results = await _mediator.Send(new GroupedProviderSearchQuery()
            {
                ApprenticeshipId = searchQueryModel.ApprenticeshipId,
                PostCode = searchQueryModel.Postcode,
                NationalProvidersOnly = searchQueryModel.NationalProvidersOnly,
                Page = searchQueryModel.Page,
                IsLevyPayingEmployer = searchQueryModel.IsLevyPayer
            });

            var model = _searchResultsViewModelMapper.Map(results, searchQueryModel);

            switch (model.Status)
            {
                case ProviderSearchResponseCodes.Success:
                case ProviderSearchResponseCodes.ScotlandPostcode:
                case ProviderSearchResponseCodes.WalesPostcode:
                case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                case ProviderSearchResponseCodes.PostCodeTerminated:
                case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                    return model;
                default:
                    throw new Exception($"Unable to get provider search response: {results.StatusCode}");
            }
        }

        public async Task<TrainingProviderSearchFilterViewModel> GetSearchFilter(TrainingProviderSearchViewModel searchQueryModel)
        {
            var results = await _mediator.Send(new GroupedProviderSearchQuery()
            {
                ApprenticeshipId = searchQueryModel.ApprenticeshipId,
                PostCode = searchQueryModel.Postcode,
                NationalProvidersOnly = searchQueryModel.NationalProvidersOnly,
                IsLevyPayingEmployer = searchQueryModel.IsLevyPayer
            });

            var model = _trainingProviderSearchFilterViewModelMapper.Map(results, searchQueryModel);

            switch (model.Status)
            {
                case ProviderSearchResponseCodes.Success:
                case ProviderSearchResponseCodes.ScotlandPostcode:
                case ProviderSearchResponseCodes.WalesPostcode:
                case ProviderSearchResponseCodes.NorthernIrelandPostcode:
                case ProviderSearchResponseCodes.PostCodeTerminated:
                case ProviderSearchResponseCodes.PostCodeInvalidFormat:
                    return model;
                default:
                    throw new Exception($"Unable to get provider search response: {results.StatusCode}");
            }
        }

        public async Task<TrainingProviderDetailsViewModel> GetDetails(TrainingProviderDetailQueryViewModel detailsQueryModel)
        {

            var cacheKey = $"FatComponentsCache-providerdetails-{detailsQueryModel.Ukprn}-{detailsQueryModel.LocationId}-{detailsQueryModel.ApprenticeshipId}";

            var cacheEntry = await _cacheService.RetrieveFromCache<TrainingProviderDetailsViewModel>(cacheKey);

            if (cacheEntry == null)
            {
                var response = await _mediator.Send(new ApprenticeshipProviderDetailQuery() { UkPrn = Convert.ToInt32(detailsQueryModel.Ukprn), ApprenticeshipId = detailsQueryModel.ApprenticeshipId, ApprenticeshipType = detailsQueryModel.ApprenticeshipType, LocationId = detailsQueryModel.LocationId });

                if (response.StatusCode == ApprenticeshipProviderDetailResponse.ResponseCodes.ApprenticeshipProviderNotFound)
                {
                    var message = $"Cannot find provider: {detailsQueryModel.Ukprn}";
                    _logger.Warn($"404 - {message}");
                    throw new HttpRequestException(message);
                }

                if (response.StatusCode == ApprenticeshipProviderDetailResponse.ResponseCodes.InvalidInput)
                {
                    var message = $"Not able to call the apprenticeship service.";
                    _logger.Warn($"{response.StatusCode} - {message}");

                    throw new HttpRequestException(message);
                }

                cacheEntry = _trainingProviderDetailsViewModelMapper.Map(response, detailsQueryModel.ApprenticeshipId);

                await _cacheService.SaveToCache(cacheKey, cacheEntry, new TimeSpan(_cacheSettings.CacheAbsoluteExpirationDays, 0, 0, 0), new TimeSpan(_cacheSettings.CacheSlidingExpirationDays, 0, 0, 0));
            }

            return cacheEntry;
        }
        public async Task<ClosestLocationsViewModel> GetClosestLocations(string apprenticeshipId, int ukprn, int locationId, string postCode)
        {
            var response = await _mediator.Send(new GetClosestLocationsQuery() { ApprenticeshipId = apprenticeshipId, Ukprn = ukprn, PostCode = postCode });

            var model = _trainingProviderClosestLocationsViewModelMapper.Map(apprenticeshipId, ukprn, locationId, postCode, response);
            
            return model;
        }
    }
}