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
                    break;
                case false:
                    switch (request.OverFiftyEmployees)
                    {
                        case true:
                            output.Funding = CalculateNonLevyFundingAndTraining(request).Funding;
                            output.Training = CalculateNonLevyFundingAndTraining(request).Training;
                            break;
                        case false:
                            output.Funding = CalculateLevyFunding(request);
                            break;
                    }
                    break;
                default: throw new InvalidOperationException("Invalid calculation input values.");
            }
            return new CalculationQueryResult
            {
                Funding = output.Funding,
                Training = output.Training != null ? output.Training : 0,
                Duration = request.TrainingCourse.Duration,
                Level = request.TrainingCourse.Level,
                Title = request.TrainingCourse.Title
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
