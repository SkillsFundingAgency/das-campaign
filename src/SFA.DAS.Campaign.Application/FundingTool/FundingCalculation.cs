using System;

namespace SFA.DAS.Campaign.Application.FundingTool
{
    public static class FundingCalculation
    {
        public static CalculationOutputValues CalculateFundingAndTraining(this CalculationInputValues input)
        {
            var output = new CalculationOutputValues() { Duration = input.TrainingCourse.Duration, Level = input.TrainingCourse.Level, Title = input.TrainingCourse.Title};
            if (input.PayBillGreaterThanThreeMillion)
            {
                var funding = Convert.ToInt32(input.TrainingCourse.MaxFunding * input.NumberRoles);
                output.Funding = funding;
            }
            else if (!input.PayBillGreaterThanThreeMillion && (bool)input.OverFiftyEmployees)
            {
                var funding = Convert.ToInt32((input.TrainingCourse.MaxFunding * input.NumberRoles) * 0.95);
                var training = Convert.ToInt32((input.TrainingCourse.MaxFunding * input.NumberRoles) * 0.05);
                output.Funding = funding;
                output.Training = training;
            }
            else if (!input.PayBillGreaterThanThreeMillion && !(bool)input.OverFiftyEmployees)
            {
                var funding = Convert.ToInt32(input.TrainingCourse.MaxFunding * input.NumberRoles);
                output.Funding = funding;
            }
            else
            {
                throw new InvalidOperationException();
            }

            return output;
        }
    }
}
