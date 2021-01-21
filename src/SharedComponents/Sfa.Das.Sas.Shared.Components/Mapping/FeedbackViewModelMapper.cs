using Sfa.Das.Sas.Shared.Components.ViewModels;
using SFA.DAS.Apprenticeships.Api.Types.Providers;
using System.Linq;
using Sfa.Das.Sas.Shared.Components.ViewModels.TrainingProvider.Details;

namespace Sfa.Das.Sas.Shared.Components.Mapping
{
    public class FeedbackViewModelMapper : IFeedbackViewModelMapper
    {
        public FeedbackViewModel Map(Feedback source)
        {
                if (source == null)
            {
                return null;
            }

            var item = new FeedbackViewModel()
            {
                ExcellentFeedbackCount = source.ExcellentFeedbackCount,
                GoodFeedbackCount = source.GoodFeedbackCount,
                PoorFeedbackCount = source.PoorFeedbackCount,
                VeryPoorFeedbackCount = source.VeryPoorFeedbackCount,
                Strengths = source.Strengths.OrderByDescending(str => str.Count).ThenBy(str => str.Name).ToList(),
                Weaknesses = source.Weaknesses.OrderByDescending(wk => wk.Count).ThenBy(wk => wk.Name).ToList(),
            };

            return item;
        }
    }
}
