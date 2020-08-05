using Sfa.Das.Sas.ApplicationServices.Responses;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface ITrainingProviderDetailsViewModelMapper
    {
        TrainingProviderDetailsViewModel Map(ApprenticeshipProviderDetailResponse source, string apprenticeshipId);
    }
}
