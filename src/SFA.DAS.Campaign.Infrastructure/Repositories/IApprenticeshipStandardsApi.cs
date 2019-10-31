using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SFA.DAS.Campaign.Infrastructure.Models;

namespace SFA.DAS.Campaign.Infrastructure.Repositories
{
    public interface IApprenticeshipStandardsApi
    {
        [Get("/api/ApprenticeshipStandards/")]
        Task<List<ApiApprenticeshipStandard>> GetAllStandardsAsync();
    }
}
