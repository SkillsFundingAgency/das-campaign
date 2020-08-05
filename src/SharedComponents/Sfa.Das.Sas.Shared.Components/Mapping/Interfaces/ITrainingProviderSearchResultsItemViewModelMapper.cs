using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Shared.Components.ViewComponents.Fat;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderSearchResultsItemViewModelMapper
    {
        TrainingProviderSearchResultsItem Map(GroupedProviderSearchResultItem item);
    }
}
