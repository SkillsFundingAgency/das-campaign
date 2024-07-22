using MediatR;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQuery : IRequest<CalculationQueryResult>
    {
        public bool PayBillGreaterThanThreeMillion { get; set; }
        public Standard TrainingCourse { get; set; }
        public int NumberRoles { get; set; }
    }
}
