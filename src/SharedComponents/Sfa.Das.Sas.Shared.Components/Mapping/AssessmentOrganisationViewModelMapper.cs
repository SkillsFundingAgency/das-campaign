using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class AssessmentOrganisationViewModelMapper : IAssessmentOrganisationViewModelMapper
    {
        public AssessmentOrganisationViewModel Map(AssessmentOrganisation item)
        {
            if (item == null) return null;

            var assessmentOrganisation = new AssessmentOrganisationViewModel();

            assessmentOrganisation.Name = item.Name;
            assessmentOrganisation.Website = item.Website;
            assessmentOrganisation.Email = item.Email;
            assessmentOrganisation.Phone = item.Phone;

            return assessmentOrganisation;
        }
    }
}
