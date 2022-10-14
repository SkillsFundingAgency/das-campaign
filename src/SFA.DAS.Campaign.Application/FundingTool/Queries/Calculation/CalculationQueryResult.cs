using MediatR;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQueryResult : IRequest<CalculationQuery>
    {
        public int Funding { get; set; }
        public int? Training { get; set; }
        public int Duration { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
    }
}
