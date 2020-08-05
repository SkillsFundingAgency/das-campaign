using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFatSearchFilterViewModelMapper
    {
        FatSearchFilterViewModel Map(ApprenticeshipSearchResults item, SearchQueryViewModel searchQueryModel);
    }
}
