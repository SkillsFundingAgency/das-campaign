using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Shared.Components.ViewModels.ApprenticeshipDetails;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFrameworkDetailsViewModelMapper
    {
        FrameworkDetailsViewModel Map(Framework item);

    }
}
