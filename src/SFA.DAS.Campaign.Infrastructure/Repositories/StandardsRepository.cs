using Ifa.Api.Api;
using Ifa.Api.Model;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.Repositories
{
    public class StandardsRepository : IStandardsRepository
    {
        private readonly IApprenticeshipProgrammeApiClient _apprenticeshipProgrammeApiClient;
        private readonly IStandardApiClient _apprenticeshipStandardApiClient;
        private readonly IStandardsMapper _standardsMapper;
        private readonly IApprenticeshipStandardsApi _ifaApprenticeshipStandardsApi;
        private readonly ICacheStorageService _cacheService;

        public StandardsRepository(IApprenticeshipProgrammeApiClient apprenticeshipProgrammeApiClient, IStandardsMapper standardsMapper, IApprenticeshipStandardsApi fullStandardsApi, ICacheStorageService cacheService, IStandardApiClient apprenticeshipStandardApiClient)
        {
            _apprenticeshipProgrammeApiClient = apprenticeshipProgrammeApiClient;
            _standardsMapper = standardsMapper;
            _ifaApprenticeshipStandardsApi = fullStandardsApi;
            _cacheService = cacheService;
            _apprenticeshipStandardApiClient = apprenticeshipStandardApiClient;
        }

        public async Task<List<StandardResultItem>> GetBySearchTerm(string searchTerm)
        {
            var result = (await _apprenticeshipProgrammeApiClient.SearchAsync(searchTerm))
                .Where(c => !string.IsNullOrEmpty(c.StandardId) && c.Published)
                .Select(_standardsMapper.Map)
                .ToList();

            return result;
        }

        public async Task<List<StandardResultItem>> GetByRoute(string routeId)
        {
            var cacheKey = "FullStandardsAPI";

            var cacheEntry = await _cacheService.RetrieveFromCache<List<ApiApprenticeshipStandard>>(cacheKey);

            if (cacheEntry == null)
            {
                // Key not in cache, so get data.
                cacheEntry = (await _ifaApprenticeshipStandardsApi.ApprenticeshipStandardsGet_3Async());

                var fatStandardIds = (await GetAllIds()).ToList();
                //Remove any null objects returned by the API and any which dont exist in FAT
                cacheEntry = cacheEntry.Where(w => w != null && fatStandardIds.Contains(w.LarsCode.ToString())).ToList();
         
                // Save data in cache.
                await _cacheService.SaveToCache(cacheKey, cacheEntry, new TimeSpan(30, 0, 0, 0), new TimeSpan(1, 0, 0, 0));
            }

            cacheEntry = cacheEntry.Where(c =>
                c.Status.ToLower() == "approved for delivery" & c.Route.ToLower() == routeId.ToLower() & c.LarsCode != 0).ToList();

            return cacheEntry.Select(_standardsMapper.Map)
                .ToList();
        }

        private async Task<IEnumerable<string>> GetAllIds()
        {
            var result = (await _apprenticeshipStandardApiClient.GetAllAsync())
                .Select(s => s.Id);

            return result;
        }
    }
}