using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.Fat.SearchFilter;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFatSearchFilterViewModelMapper
    {
        FatSearchFilterViewModel Map(ApprenticeshipSearchResults item, SearchQueryViewModel searchQueryModel);
    }
}
