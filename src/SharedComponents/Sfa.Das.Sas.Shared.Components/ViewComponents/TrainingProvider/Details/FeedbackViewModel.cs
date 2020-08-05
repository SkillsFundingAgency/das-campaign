using System;
using System.Collections.Generic;
using SFA.DAS.Apprenticeships.Api.Types.Providers;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class FeedbackViewModel : Feedback
    {
        public FeedbackViewModel()
        {
        }

        public ICollection<ProviderAttribute> Strengths { get; set; }
        public ICollection<ProviderAttribute> Weaknesses { get; set; }
        public int ExcellentFeedbackCount { get; set; }
        public int GoodFeedbackCount { get; set; }
        public int PoorFeedbackCount { get; set; }
        public int VeryPoorFeedbackCount { get; set; }
        public int TotalFeedbackCount => ExcellentFeedbackCount + GoodFeedbackCount + PoorFeedbackCount + VeryPoorFeedbackCount;
        public decimal ExcellentFeedbackPercentage => CalculatePercentageOfTotal(ExcellentFeedbackCount);
        public decimal GoodFeedbackPercentage => CalculatePercentageOfTotal(GoodFeedbackCount);
        public decimal PoorFeedbackPercentage => CalculatePercentageOfTotal(PoorFeedbackCount);
        public decimal VeryPoorFeedbackPercentage => CalculatePercentageOfTotal(VeryPoorFeedbackCount);

        private decimal CalculatePercentageOfTotal(int feedbackCount)
        {
            return Math.Round((decimal)(feedbackCount * 100) / TotalFeedbackCount, 2);
        }
    }
}