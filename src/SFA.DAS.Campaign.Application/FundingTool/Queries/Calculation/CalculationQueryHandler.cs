using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQueryHandler : IRequestHandler<CalculationQuery, CalculationQueryResult>
    {
        public async Task<CalculationQueryResult> Handle(CalculationQuery request, CancellationToken cancellationToken)
        {
            var output = new CalculationQueryResult();

            switch (request.PayBillGreaterThanThreeMillion)
            {
                case true:
                    output.Funding = CalculateLevyFunding(request);
                    output.Training = 0;
                    break;
                case false:
                    output.Funding = CalculateNonLevyFundingAndTraining(request).Funding;
                    output.Training = CalculateNonLevyFundingAndTraining(request).Training;
                    break;
                default: throw new InvalidOperationException("Invalid calculation input values.");
            }

            return new CalculationQueryResult
            {
                Funding = output.Funding,
                Training = output.Training,
                StandardDuration = request.TrainingCourse.Duration,
                StandardLevel = request.TrainingCourse.Level,
                StandardTitle = request.TrainingCourse.Title
            };
        }

        public int CalculateLevyFunding(CalculationQuery query)
        {
            return Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles);
        }

        public CalculationQueryResult CalculateNonLevyFundingAndTraining(CalculationQuery query)
        {
            var funding = Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.95);
            var training = Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.05);
            return new CalculationQueryResult() { Funding = funding, Training = training };
        }
    }
}
