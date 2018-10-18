using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Domain.ApprenticeshipCourses
{
    public interface IStandardsService
    {
        Task<List<StandardResultItem>> GetBySearchTerm(string searchTerm);
    }
}