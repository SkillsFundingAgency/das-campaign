using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewModels;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Details;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderDetailsViewModelMapper
    {
        TrainingProviderDetailsViewModel Map(ApprenticeshipProviderDetailResponse source, string apprenticeshipId);
    }
}
