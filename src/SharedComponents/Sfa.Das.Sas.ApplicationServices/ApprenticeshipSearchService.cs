using System.Threading.Tasks;

namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Collections.Generic;
    using Logging;
    using Models;
    using Settings;
    using SFA.DAS.NLog.Logger;

    public sealed class ApprenticeshipSearchService : IApprenticeshipSearchService
    {
        private readonly IApprenticeshipSearchProvider _searchProvider;
        private readonly ILog _logger;
        private readonly IPaginationSettings _paginationSettings;

        public ApprenticeshipSearchService(
            IApprenticeshipSearchProvider searchProvider,
            ILog logger,
            IPaginationSettings paginationSettings)
        {
            _searchProvider = searchProvider;
            _logger = logger;
            _paginationSettings = paginationSettings;
        }

        public async Task<ApprenticeshipSearchResults> SearchByKeyword(string keywords, int page, int take, int order, List<int> selectedLevels)
        {
            var takeElements = take == 0 ? _paginationSettings.DefaultResultsAmount : take;
            var results = await _searchProvider.SearchByKeyword(keywords, page, takeElements, order, selectedLevels);

             _logger.Info(
                "Apprenticeship Keyword Search",
                new ApprenticeshipSearchLogEntry { TotalHits = results?.TotalResults ?? -1, Keywords = keywords?.Split(' ') ?? new[] { "[empty]" } });

            return results;
        }
    }
}