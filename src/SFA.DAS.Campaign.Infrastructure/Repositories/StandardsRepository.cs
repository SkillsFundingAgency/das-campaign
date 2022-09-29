#nullable enable
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using Standard = SFA.DAS.Campaign.Domain.Content.Standard;

namespace SFA.DAS.Campaign.Infrastructure.Repositories
{
    public class StandardsRepository : IStandardsRepository
    {
        private readonly IApiClient _apiClient;

        public StandardsRepository(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<string>> GetRoutes()
        {
            var result = await _apiClient.Get<GetSectorsResponse>(new GetSectorsRequest());

            return result.Sectors.Select(c => c.Route).ToList();
        }

        public async Task<List<Standard>> GetStandards(string? routeId)
        {
            var result = await _apiClient.Get<GetStandardsResponse>(new GetStandardsBySectorRequest(routeId));
            var standards = result.Standards.Select(c => new Standard()
            {
                Title = c.Title,
                StandardUId = c.StandardUId,
                LarsCode = c.LarsCode,
                Level = c.Level
            })
                .ToList();
            return standards;
        }
    }
}