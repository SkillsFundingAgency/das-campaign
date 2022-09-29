using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IStandardsRepository
    {
        Task<List<int>> GetByRoute(string routeId);
        Task<List<string>> GetRoutes();
        //Task<GetStandardsResponse> GetStandards(); 
    }
}