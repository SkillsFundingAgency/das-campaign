#nullable enable
using SFA.DAS.Campaign.Domain.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IStandardsRepository
    {
        Task<List<StandardResponse>> GetStandards(string? routeId = null);
        Task<StandardResponse> GetStandard(string standardUId);

        Task<List<string>> GetRoutes();
    }
}