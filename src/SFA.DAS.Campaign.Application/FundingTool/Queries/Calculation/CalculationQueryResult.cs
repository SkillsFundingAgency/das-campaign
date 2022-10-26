#nullable enable
using MediatR;
using System;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQueryResult : IRequest<CalculationQuery>
    {
        public int Funding { get; set; }
        public string FundingOutput { get { return String.Format("{0:#,##0.##}", Funding); } }
        public int? Training { get; set; }
        public string? TrainingOutput { get { return String.Format("{0:#,##0.##}", Training); } }
        public int Duration { get; set; }
        public int Level { get; set; }
        public string Title { get; set; } = null!;
    }
}
