namespace Sfa.Das.Sas.Infrastructure.Mapping
{
    using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;
    using Sfa.Das.Sas.Core.Domain;

    public interface IAssessmentOrganisationMapping
    {
        AssessmentOrganisation Map(Organisation document);
    }
}