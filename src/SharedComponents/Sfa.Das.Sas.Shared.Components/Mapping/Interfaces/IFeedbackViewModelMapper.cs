using Sfa.Das.Sas.Shared.Components.ViewModels;
using SFA.DAS.Apprenticeships.Api.Types.Providers;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Details;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFeedbackViewModelMapper
    {
        FeedbackViewModel Map(Feedback source);
    }
}
