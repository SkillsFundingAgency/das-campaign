using System;
using Sfa.Das.Sas.ApplicationServices.Responses;

namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using Settings;
    using SFA.DAS.NLog.Logger;

    public class ProviderNameSearchService : IProviderNameSearchService
    {
        private readonly IPaginationSettings _paginationSettings;
        private readonly IProviderSearchProvider _searchProviderName;
        private readonly ILog _logger;
        public ProviderNameSearchService(IPaginationSettings paginationSettings, IProviderSearchProvider searchProviderName, ILog logger)
        {
            _paginationSettings = paginationSettings;
            _searchProviderName = searchProviderName;
            _logger = logger;
        }

        public async Task<ProviderNameSearchResultsAndPagination> SearchProviderNameAndAliases(string searchTerm, int page, int? pageSize)
        {
            if (pageSize == null)
            {
                pageSize = _paginationSettings.DefaultResultsAmount;
            }

            _logger.Info($"Provider Name Search started: SearchTerm: [{searchTerm}], Page: [{page}], Page Size: [{pageSize}]");

            try
            {
                var results = await _searchProviderName.SearchProviderNameAndAliases(searchTerm, page, pageSize.Value);

                _logger.Info($"Provider Name Search complete: SearchTerm: [{searchTerm}], Page: [{results.ActualPage}], Page Size: [{pageSize}], Total Results: [{results.TotalResults}]");

                return results;
            }
            catch (Exception e)
            {
                _logger.Error(e,$"Provider Name Search error: SearchTerm: [{searchTerm}], Page: [{page}], Page Size: [{pageSize}]");
            return new ProviderNameSearchResultsAndPagination()
            {
                HasError = true,
                ResponseCode = ProviderNameSearchResponseCodes.SearchFailed
            };
            }
        }
    }
}
