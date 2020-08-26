namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.ApplicationServices.Exceptions;
    using Sfa.Das.Sas.ApplicationServices.Logging;
    using Sfa.Das.Sas.ApplicationServices.Models;
    using Sfa.Das.Sas.ApplicationServices.Settings;
    using Sfa.Das.Sas.Core.Domain.Model;
    using Sfa.Das.Sas.Core.Domain.Services;

    public sealed class ProviderSearchService : IProviderSearchService
    {
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;
        private readonly ILookupLocations _postCodeLookup;
        private readonly ILog _logger;
        private readonly IPaginationSettings _paginationSettings;
        private readonly IProviderSearchProvider _providerSearchProvider;

        public ProviderSearchService(
            IGetStandards getStandards,
            IGetFrameworks getFrameworks,
            ILookupLocations postcodeLookup,
            ILog logger,
            IPaginationSettings paginationSettings,
            IProviderSearchProvider providerSearchProvider)
        {
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _postCodeLookup = postcodeLookup;
            _logger = logger;
            _paginationSettings = paginationSettings;
            _providerSearchProvider = providerSearchProvider;
        }
        public async Task<ProviderSearchResults> SearchProviders(string apprenticeshipId, string postCode, Pagination pagination, IEnumerable<string> deliveryModes, bool hasNonLevyContract, bool showNationalOnly, int orderBy = 0)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                return new ProviderSearchResults { ApprenticeshipId = apprenticeshipId, PostCodeMissing = true };
            }

            int apprenticeshipIdInt;

            IApprenticeshipProduct apprenticeship;

            if (int.TryParse(apprenticeshipId, out apprenticeshipIdInt))
            {
                apprenticeship = _getStandards.GetStandardById(apprenticeshipId);
            }
            else
            {
                apprenticeship = _getFrameworks.GetFrameworkById(apprenticeshipId);
            }

            if (apprenticeship == null)
            {
                return GetProviderSearchResultErrorResponse(apprenticeshipId, null, postCode, LocationLookupResponse.ApprenticeshipNotFound);
            }

            try
            {
                var coordinateResponse = await _postCodeLookup.GetLatLongFromPostCode(postCode);
                var coordinates = coordinateResponse.Coordinate;


                if (coordinateResponse.ResponseCode != LocationLookupResponse.Ok)
                {
                    return GetProviderSearchResultErrorResponse(apprenticeshipId, apprenticeship?.Title, postCode, coordinateResponse.ResponseCode);
                }

                var takeElements = pagination.Take == 0 ? _paginationSettings.DefaultResultsAmount : pagination.Take;

                LogSearchRequest(postCode, coordinates);

                var filter = new ProviderSearchFilter
                {
                    DeliveryModes = deliveryModes,
                    HasNonLevyContract = hasNonLevyContract,
                    ShowNationalOnly = showNationalOnly
                };

                var searchResults = await _providerSearchProvider.SearchProvidersByLocation(apprenticeshipId, coordinates, pagination.Page, takeElements, filter, orderBy);

                var result = new ProviderSearchResults
                {
                    TotalResults = searchResults.Total,
                    ResultsToTake = takeElements,
                    ApprenticeshipId = apprenticeshipId,
                    Title = apprenticeship?.Title,
                    Level = apprenticeship.Level,
                    PostCode = postCode,
                    TrainingOptionsAggregation = searchResults.TrainingOptionsAggregation,
                    NationalProviders = searchResults.NationalProvidersAggregation,
                    SelectedTrainingOptions = deliveryModes,
                    ResponseCode = LocationLookupResponse.Ok,
                    ShowNationalProvidersOnly = false,
                    Hits = searchResults.Hits,
                    LastPage = takeElements > 0 ? (int)System.Math.Ceiling((double)searchResults.Total / takeElements) : 1
                };

                return result;
            }
            catch (SearchException ex)
            {
                _logger.Error(ex, "Search for provider failed.");

                return GetProviderSearchResultErrorResponse(apprenticeshipId, apprenticeship?.Title, postCode, ServerLookupResponse.InternalServerError);
            }
        }

        public async Task<GroupedProviderSearchResults> SearchProvidersWithGroupedResults(string apprenticeshipId, string postCode, Pagination pagination, bool hasNonLevyContract, bool showNationalOnly)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                return new GroupedProviderSearchResults { PostCodeMissing = true };
            }

            try
            {
                var coordinateResponse = await _postCodeLookup.GetLatLongFromPostCode(postCode);
                var coordinates = coordinateResponse.Coordinate;


                if (coordinateResponse.ResponseCode != LocationLookupResponse.Ok)
                {
                    return GetGroupedProviderSearchResultErrorResponse(postCode, coordinateResponse.ResponseCode);
                }

                var takeElements = pagination.Take == 0 ? _paginationSettings.DefaultResultsAmount : pagination.Take;

                LogSearchRequest(postCode, coordinates);

                var filter = new ProviderSearchFilter
                {
                    DeliveryModes = null,
                    HasNonLevyContract = hasNonLevyContract,
                    ShowNationalOnly = showNationalOnly
                };

                var searchResults = await _providerSearchProvider.SearchProvidersByLocationGroupByProvider(apprenticeshipId, coordinates, pagination.Page, takeElements, filter);

                var result = new GroupedProviderSearchResults
                {
                    TotalResults = searchResults.Total,
                    ResultsToTake = takeElements,
                    PostCode = postCode,
                    ShowNationalProvidersOnly = false,
                    Hits = searchResults.Hits,
                    LastPage = takeElements > 0 ? (int)System.Math.Ceiling((double)searchResults.Total / takeElements) : 1,
                    HasNationalProviders = searchResults.HasNationalProviders
                };

                return result;
            }
            catch (SearchException ex)
            {
                _logger.Error(ex, "Search for provider failed.");

                return GetGroupedProviderSearchResultErrorResponse(postCode, ServerLookupResponse.InternalServerError);
            }
        }

        private static ProviderSearchResults GetProviderSearchResultErrorResponse(string apprenticeshipId, string Title, string postCode, string responseCode)
        {

            var errorResponse = new ProviderSearchResults
            {
                TotalResults = 0,
                ApprenticeshipId = apprenticeshipId,
                Title = Title,
                PostCode = postCode,
                Hits = new ProviderSearchResultItem[0],
                ResponseCode = responseCode
            };
            return errorResponse;
        }

        private static GroupedProviderSearchResults GetGroupedProviderSearchResultErrorResponse(string postCode, string responseCode)
        {
            var errorResponse = new GroupedProviderSearchResults
            {
                TotalResults = 0,
                PostCode = postCode,
                Hits = new GroupedProviderSearchResultItem[0],
                ResponseCode = responseCode
            };
            return errorResponse;
        }

        private void LogSearchRequest(string postCode, Coordinate coordinates)
        {
            var logEntry = new ApprenticeshipSearchLogEntry
            {
                Postcode = postCode,
                Coordinates = new[] { coordinates.Lon, coordinates.Lat }
            };

            _logger.Info("Provider location search", logEntry);
        }
    }
}
