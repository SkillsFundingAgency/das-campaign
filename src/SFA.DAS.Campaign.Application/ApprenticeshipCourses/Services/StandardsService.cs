using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;

namespace SFA.DAS.Campaign.Application.ApprenticeshipCourses.Services
{
    public class StandardsService : IStandardsService
    {
        private readonly IApprenticeshipProgrammeApiClient _apprenticeshipProgrammeApiClient;
        private readonly IStandardsMapper _standardsMapper;

        public StandardsService(IApprenticeshipProgrammeApiClient apprenticeshipProgrammeApiClient, IStandardsMapper standardsMapper)
        {
            _apprenticeshipProgrammeApiClient = apprenticeshipProgrammeApiClient;
            _standardsMapper = standardsMapper;
        }

        public async Task<List<StandardResultItem>> GetBySearchTerm(string searchTerm)
        {
            var result = (await _apprenticeshipProgrammeApiClient.SearchAsync(searchTerm))
                .Where(c => !string.IsNullOrEmpty(c.StandardId) && c.Published)
                .Select(_standardsMapper.Map)
                .ToList();

            return result;
        }
    }
}