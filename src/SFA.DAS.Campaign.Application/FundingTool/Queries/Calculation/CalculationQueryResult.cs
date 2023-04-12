#nullable enable
using MediatR;
using System;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQueryResult : IRequest<CalculationQuery>
    {
        public int Funding { get; set; }
        public string AvailableFunding { get { return String.Format("{0:#,##0.##}", Funding); } }
        public int? Training { get; set; }
        public string? TrainingCost { get { return String.Format("{0:#,##0.##}", Training); } }
        public int StandardDuration { get; set; }
        public int StandardLevel { get; set; }
        public string StandardTitle { get; set; } = null!;
    }
}
