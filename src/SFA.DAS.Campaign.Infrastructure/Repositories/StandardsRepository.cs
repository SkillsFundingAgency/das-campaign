using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
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

        async Task<List<int>> IStandardsRepository.GetByRoute(string routeId)
        {
            var result = await _apiClient.Get<GetStandardsResponse>(new GetStandardsBySectorRequest(routeId));
            return result.Standards.Select(x => x.LarsCode).ToList();
        }

        //public async Task<GetStandardsResponse> GetStandards()
        //{
        //    var result = await _apiClient.Get<GetStandardsResponse>(new GetStandardsRequest());
        //    return result;
        //}
    }
}