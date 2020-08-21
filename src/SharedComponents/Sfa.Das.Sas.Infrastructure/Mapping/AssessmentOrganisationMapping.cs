using Sfa.Das.Sas.Core.Domain;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    public class AssessmentOrganisationMapping : IAssessmentOrganisationMapping
    {
        public AssessmentOrganisation Map(Organisation document)
        {
            if (document == null) return null;

            var assessmentOrgainisation = new AssessmentOrganisation()
            {
                Name = document.Name,
                Email = document.Email,
                Phone = document.Phone,
                Website = document.Website
            };
            return assessmentOrgainisation;
        }
    }
}
