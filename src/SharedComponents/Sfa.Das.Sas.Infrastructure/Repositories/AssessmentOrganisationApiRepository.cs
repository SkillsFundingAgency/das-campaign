using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Infrastructure.Mapping;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
using SFA.DAS.AssessmentOrgs.Api.Client;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Infrastructure.Repositories
{
    public sealed class AssessmentOrganisationApiRepository : IAssessmentOrgsApiClient, IGetAssessmentOrganisations
    {
        private readonly IAssessmentOrgsApiClient _apiClient;
        private readonly ILog _logger;
        private readonly IAssessmentOrganisationMapping _assessmentOrganisationMapping;

        public AssessmentOrganisationApiRepository(IAssessmentOrgsApiClient apiClient, ILog logger, IAssessmentOrganisationMapping assessmentOrganisationMapping)
        {
            _apiClient = apiClient;
            _logger = logger;
            _assessmentOrganisationMapping = assessmentOrganisationMapping;
        }

        public void Dispose()
        {
            _apiClient.Dispose();
        }

        public Organisation Get(string organisationId)
        {
            return _apiClient.Get(organisationId);
        }

        public async Task<Organisation> GetAsync(string organisationId)
        {
            return await _apiClient.GetAsync(organisationId);
        }

        public IEnumerable<Organisation> ByStandard(int standardId)
        {
            return _apiClient.ByStandard(standardId);
        }

        public async Task<IEnumerable<Organisation>> ByStandardAsync(int standardId)
        {
            return await _apiClient.ByStandardAsync(standardId);
        }

        public IEnumerable<Organisation> ByStandard(string standardId)
        {
            return _apiClient.ByStandard(standardId);
        }

        public async Task<IEnumerable<Organisation>> ByStandardAsync(string standardId)
        {
            return await _apiClient.ByStandardAsync(standardId);
        }

        public IEnumerable<OrganisationSummary> FindAll()
        {
            return _apiClient.FindAll();
        }

        public async Task<IEnumerable<OrganisationSummary>> FindAllAsync()
        {
            return await _apiClient.FindAllAsync();
        }

        public bool Exists(string organisationId)
        {
            return _apiClient.Exists(organisationId);
        }

        public async Task<bool> ExistsAsync(string organisationId)
        {
            return await _apiClient.ExistsAsync(organisationId);
        }

        public IEnumerable<StandardOrganisationSummary> FindAllStandardsByOrganisationId(string organisationId)
        {
            return _apiClient.FindAllStandardsByOrganisationId(organisationId);
        }

        public async Task<IEnumerable<StandardOrganisationSummary>> FindAllStandardsByOrganisationIdAsync(string organisationId)
        {
            return await _apiClient.FindAllStandardsByOrganisationIdAsync(organisationId);
        }

        public async Task<IEnumerable<AssessmentOrganisation>> GetByStandardId(int id)
        {
                var organisations = await this.ByStandardAsync(id);
                return organisations.Select(_assessmentOrganisationMapping.Map);
        }
    }
}
