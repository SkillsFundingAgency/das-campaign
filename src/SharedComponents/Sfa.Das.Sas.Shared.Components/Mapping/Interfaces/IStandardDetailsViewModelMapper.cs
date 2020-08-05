using System.Collections.Generic;
using Sfa.Das.Sas.Core.Domain;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewComponents.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IStandardDetailsViewModelMapper
    {
        StandardDetailsViewModel Map(Standard item,IEnumerable<AssessmentOrganisation> assessmentOrganisations);

    }
}
