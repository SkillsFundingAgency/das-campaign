using System;

namespace SFA.DAS.Campaign.Application.FundingTool
{
    public static class FundingCalculation
    {
        public static CalculationOutputValues CalculateFundingAndTraining(this CalculationInputValues input)
        {
            var output = new CalculationOutputValues() { Duration = input.TrainingCourse.Duration, Level = input.TrainingCourse.Level, Title = input.TrainingCourse.Title };

            switch (input.PayBillGreaterThanThreeMillion)
            {
                case true:
                    output.Funding = CalculateLevyFunding(input);
                    break;
                case false:
                    switch (input.OverFiftyEmployees)
                    {
                        case true:
                            output.Funding = CalculateNonLevyFundingAndTraining(input).Funding;
                            output.Training = CalculateNonLevyFundingAndTraining(input).Training;
                            break;
                        case false:
                            output.Funding = CalculateLevyFunding(input);
                            break;
                    }
                    break;
                default: throw new InvalidOperationException("Invalid calculation input values.");
            }
            return output;
        }

        public static int CalculateLevyFunding(CalculationInputValues input)
        {
            return Convert.ToInt32(input.TrainingCourse.MaxFunding * input.NumberRoles);
        }

        public static CalculationOutputValues CalculateNonLevyFundingAndTraining(CalculationInputValues input)
        {
            var funding = Convert.ToInt32((input.TrainingCourse.MaxFunding * input.NumberRoles) * 0.95);
            var training = Convert.ToInt32((input.TrainingCourse.MaxFunding * input.NumberRoles) * 0.05);
            return new CalculationOutputValues() { Funding = funding, Training = training };
        }
    }
}
