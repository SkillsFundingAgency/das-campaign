using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider
{
    public class TrainingProviderSearchFilterViewModel : TrainingProviderSearchViewModel
    {
        public ProviderSearchResponseCodes Status { get; set; }

        public bool HasNationalProviders { get; set; }
    }
}
