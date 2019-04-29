using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ifa.Api;
using Ifa.Api.Model;
using Microsoft.Extensions.Caching.Memory;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services
{
    public class StandardsService : IStandardsService
    {
        private readonly IApprenticeshipProgrammeApiClient _apprenticeshipProgrammeApiClient;
        private readonly IStandardsMapper _standardsMapper;
        private readonly IFullStandardsApi _fullStandardsApi;
        private readonly IMemoryCache _memoryCache;

        public StandardsService(IApprenticeshipProgrammeApiClient apprenticeshipProgrammeApiClient, IStandardsMapper standardsMapper, IFullStandardsApi fullStandardsApi, IMemoryCache memoryCache)
        {
            _apprenticeshipProgrammeApiClient = apprenticeshipProgrammeApiClient;
            _standardsMapper = standardsMapper;
            _fullStandardsApi = fullStandardsApi;
            _memoryCache = memoryCache;
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
            

            if (!_memoryCache.TryGetValue(cacheKey, out List<TempApprenticeshipStandard> cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = (await _fullStandardsApi.FullStandardsGetAllAsync());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(new TimeSpan(1, 0, 0, 0))
                    .SetSlidingExpiration(new TimeSpan(3, 0, 0));
                
                // Save data in cache.
                _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            var result = cacheEntry;

            result = result.Where(c => c.IsPublished == true & c.Route.ToLower() == routeId.ToLower()).ToList();


            return result.Select(_standardsMapper.Map)
                .ToList();
        }
    }
}