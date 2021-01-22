using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Campaign.Infrastructure.Models;
using SFA.DAS.Campaign.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.Repositories
{
    public class StandardsRepository : IStandardsRepository
    {
        
        public StandardsRepository()
        {
            
        }

        public async Task<List<string>> GetRoutes()
        {
            return new List<string>();
        }

        public async Task<List<StandardResultItem>> GetByRoute(string routeId)
        {
            //var standardIds = await GetAll();

            //var standardRoutes = standardIds.Where(w => w.Route.ToLower() == routeId.ToLower()).ToList();
            return new List<StandardResultItem>();
        }

    }
}