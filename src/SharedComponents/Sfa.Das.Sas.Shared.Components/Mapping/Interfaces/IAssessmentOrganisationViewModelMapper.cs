using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Shared.Components.ViewModels.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IAssessmentOrganisationViewModelMapper
    {
        AssessmentOrganisationViewModel Map(AssessmentOrganisation assessmentOrganisation);

    }
}
