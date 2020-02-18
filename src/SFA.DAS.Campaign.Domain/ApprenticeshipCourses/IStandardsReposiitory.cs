using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IStandardsRepository
    {
        Task<List<StandardResultItem>> GetBySearchTerm(string searchTerm);
        Task<List<StandardResultItem>> GetByRoute(string routeId);
        Task<List<StandardResultItem>> GetAll();
    }
}