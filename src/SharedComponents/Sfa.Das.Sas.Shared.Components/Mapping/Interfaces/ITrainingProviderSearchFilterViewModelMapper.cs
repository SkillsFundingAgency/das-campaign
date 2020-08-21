using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Search;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.SearchFilter;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderSearchFilterViewModelMapper
    {
        TrainingProviderSearchFilterViewModel Map(GroupedProviderSearchResponse item, TrainingProviderSearchViewModel searchQueryModel);

    }
}
