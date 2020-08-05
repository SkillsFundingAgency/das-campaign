using Sfa.Das.Sas.Shared.Components.ViewModels;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public interface IFeedbackViewModelMapper
    {
        FeedbackViewModel Map(Feedback source);
    }
}
