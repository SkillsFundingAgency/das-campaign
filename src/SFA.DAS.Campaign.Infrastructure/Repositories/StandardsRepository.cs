using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

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

            return result.Sectors.Select(c=>c.Route).ToList();
        }

        public async Task<List<int>> GetByRoute(string routeId)
        {
            var result = await _apiClient.Get<GetStandardsResponse>(new GetStandardsBySectorRequest(routeId));
            return result.Standards.Select(x=>x.Id).ToList();
        }

    }
}