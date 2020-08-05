using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels.Apprenticeship;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IApprenticeshipItemViewModelMapper
    {
        ApprenticeshipItemViewModel Map(ApprenticeshipSearchResultsItem item);

    }
}
