using System;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices;
using Sfa.Das.Sas.Shared.Components.Mapping;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.ApplicationServices.Services;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.Orchestrators
{
    public class FatOrchestrator : IFatOrchestrator
    {
        private readonly IApprenticeshipSearchService _apprenticeshipSearchService;
        private readonly IFatSearchResultsViewModelMapper _fatSearchResultsViewModelMapper;
        private readonly IFatSearchFilterViewModelMapper _fatSearchFilterViewModelMapper;
        private ICacheStorageService _cacheService;
        private readonly ICacheSettings _cacheSettings;

        public FatOrchestrator(IApprenticeshipSearchService apprenticeshipSearchService, IFatSearchResultsViewModelMapper fatSearchResultsViewModelMapper, IFatSearchFilterViewModelMapper fatSearchFilterViewModelMapper, ICacheStorageService cacheService, ICacheSettings cacheSettings)
        {
            _apprenticeshipSearchService = apprenticeshipSearchService;
            _fatSearchResultsViewModelMapper = fatSearchResultsViewModelMapper;
            _fatSearchFilterViewModelMapper = fatSearchFilterViewModelMapper;
            _cacheService = cacheService;
            _cacheSettings = cacheSettings;
        }

        public async Task<FatSearchResultsViewModel> GetSearchResults(SearchQueryViewModel searchQueryModel)
        {

            
            var cacheKey = $"FatComponentsCache-searchresults-{searchQueryModel.Keywords}-{searchQueryModel.Page}-{searchQueryModel.ResultsToTake}-{searchQueryModel.SortOrder}";

            foreach (var level in searchQueryModel.SelectedLevels)
            {
                cacheKey += $"-{level}";
            }

            var model = await _cacheService.RetrieveFromCache<FatSearchResultsViewModel>(cacheKey);

            if (model == null)
            {
                var results = _apprenticeshipSearchService.SearchByKeyword(searchQueryModel.Keywords, searchQueryModel.Page, searchQueryModel.ResultsToTake, searchQueryModel.SortOrder, searchQueryModel.SelectedLevels);

                model = _fatSearchResultsViewModelMapper.Map(await results);

                await _cacheService.SaveToCache(cacheKey, model, new TimeSpan(_cacheSettings.CacheAbsoluteExpirationDays, 0, 0, 0), new TimeSpan(_cacheSettings.CacheSlidingExpirationDays, 0, 0, 0));
            }

            model.SearchQuery.AddRemoveBasketResponse = searchQueryModel.AddRemoveBasketResponse;
            
            return model;
        }

        public async Task<FatSearchFilterViewModel> GetSearchFilters(SearchQueryViewModel searchQueryModel)
        {
            var results = _apprenticeshipSearchService.SearchByKeyword(searchQueryModel.Keywords, searchQueryModel.Page, searchQueryModel.ResultsToTake, searchQueryModel.SortOrder, searchQueryModel.SelectedLevels);

            var model = _fatSearchFilterViewModelMapper.Map(await results, searchQueryModel);

            return model;
        }
    }
}