using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderSearchFilterViewModelMapper
    {
        TrainingProviderSearchFilterViewModel Map(GroupedProviderSearchResponse item, TrainingProviderSearchViewModel searchQueryModel);

    }
}
