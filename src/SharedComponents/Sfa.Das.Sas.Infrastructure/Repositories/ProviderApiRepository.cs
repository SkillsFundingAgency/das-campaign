using SFA.DAS.Apprenticeships.Api.Types.Exceptions;

namespace Sfa.Das.Sas.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SFA.DAS.Apprenticeships.Api.Types.Providers;
    using Sfa.Das.FatApi.Client.Api;
    using SFA.DAS.NLog.Logger;
    using SFA.DAS.Providers.Api.Client;
    using Sfa.Das.Sas.ApplicationServices;
    using Sfa.Das.Sas.ApplicationServices.Models;
    using Sfa.Das.Sas.ApplicationServices.Responses;
    using Sfa.Das.Sas.Core.Domain.Model;
    using Sfa.Das.Sas.Core.Domain.Services;
    using Sfa.Das.Sas.Infrastructure.Mapping;

    public class ProviderApiRepository : IGetProviderDetails, IProviderSearchProvider
    {

        private readonly IProviderApiClient _providerApiClient;
        private readonly IProvidersVApi _providersV3Api;
        private readonly ISearchV3Api _searchV3Api;
        private readonly ISearchV4Api _searchV4Api;
        private readonly ISearchResultsMapping _searchResultsMapping;
        private readonly IProviderNameSearchMapping _providerNameSearchMapping;
        private readonly ILog _logger;

        public ProviderApiRepository(IProviderApiClient providerApiClient, IProvidersVApi providersV3Api, ISearchResultsMapping searchResultsMapping, ILog logger, ISearchV3Api searchV3Api, ISearchV4Api searchV4Api, IProviderNameSearchMapping providerNameSearchMapping)
        {
            _providerApiClient = providerApiClient;
            _providersV3Api = providersV3Api;
            _searchResultsMapping = searchResultsMapping;
            _logger = logger;
            _searchV3Api = searchV3Api;
            _searchV4Api = searchV4Api;
            _providerNameSearchMapping = providerNameSearchMapping;
        }

        public async Task<SFA.DAS.Apprenticeships.Api.Types.Providers.Provider> GetProviderDetails(long ukPrn)
        {
            try
            {
                var result = await _providerApiClient.GetAsync(ukPrn);
                return result;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.Error(ex,$"Unable to get provider with ukprn: {ukPrn}");
                return null;
            }
        }

        public IEnumerable<ProviderSummary> GetAllProviders()
        {
            var res = _providerApiClient.FindAll();
            return res;
        }

        public async Task<ApprenticeshipTrainingSummary> GetApprenticeshipTrainingSummary(long ukprn, int pageNumber)
        {
            return await _providerApiClient.GetActiveApprenticeshipTrainingByProviderAsync(ukprn, pageNumber);
        }

        public async Task<ProviderSearchResult<ProviderSearchResultItem>> SearchProvidersByLocation(string apprenticeshipId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter, int orderBy = 0)
        {
            var deliveryModes = new List<string>();

            foreach (var filterDeliveryMode in filter.DeliveryModes)
            {
                switch (filterDeliveryMode)
                {
                    case "dayrelease":
                        deliveryModes.Add("0");
                        break;
                    case "blockrelease":
                        deliveryModes.Add("1");
                        break;
                    case "100percentemployer":
                        deliveryModes.Add("2");
                        break;
                    default:
                        deliveryModes.Add(filterDeliveryMode);
                        break;
                }
            }

            var result = await _providersV3Api.GetByApprenticeshipIdAndLocationAsync(apprenticeshipId, coordinates.Lat, coordinates.Lon, page, take, filter.HasNonLevyContract, filter.ShowNationalOnly, string.Join(",", deliveryModes), orderBy);

            return _searchResultsMapping.Map(result);
        }

        public async Task<GroupedProviderSearchResult<GroupedProviderSearchResultItem>> SearchProvidersByLocationGroupByProvider(string apprenticeshipId, Coordinate coordinates, int page, int take, ProviderSearchFilter filter)
        {
            var result = await _searchV4Api.GetByApprenticeshipIdAndLatLon(apprenticeshipId, coordinates.Lat, coordinates.Lon, page, take, filter.HasNonLevyContract, filter.ShowNationalOnly);

            return _searchResultsMapping.Map(result);
        }

        public async Task<ProviderNameSearchResultsAndPagination> SearchProviderNameAndAliases(string searchTerm, int page, int pageSize)
        {
            var results = new ProviderNameSearchResultsAndPagination();

            if (searchTerm.Length < 3)
            {
                _logger.Info(
                    $"Search term causing SearchTermTooShort: [{searchTerm}]");

                results.SearchTerm = searchTerm;
                results.ResponseCode = ProviderNameSearchResponseCodes.SearchTermTooShort;

                return results;
            }

            _logger.Info(
                $"Provider Name Search started: SearchTerm: [{searchTerm}], Page: [{page}], Page Size: [{pageSize}]");

            try
            {
                var apiResults = await _searchV3Api.SearchProviderNameAsync(searchTerm, page, pageSize);

                results = _providerNameSearchMapping.Map(apiResults, searchTerm);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Provider Name Search error: SearchTerm: [{searchTerm}], Page: [{page}], Page Size: [{pageSize}]");
                results.ResponseCode = ProviderNameSearchResponseCodes.SearchFailed;
                results.HasError = true;
                return results;
            }

            _logger.Info(
                $"Provider Name Search complete: SearchTerm: [{searchTerm}], Page: [{results.ActualPage}], Page Size: [{pageSize}], Total Results: [{results.TotalResults}]");

            return results;
        }

        public async Task<ProviderLocationsSearchResult> GetClosestLocations(string apprenticeshipId, long ukprn, Coordinate searchPoint)
        {
            var result = await _searchV4Api.GetClosestProviderLocationsThatCoverPointForApprenticeship(ukprn, apprenticeshipId, searchPoint.Lat, searchPoint.Lon);

            return _searchResultsMapping.Map(result);
        }
    }
}
