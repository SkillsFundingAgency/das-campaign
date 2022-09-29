using System;

namespace SFA.DAS.Campaign.Application.FundingTool
{
    public static class FundingCalculation
    {
        public static CalculationOutputValues CalculateFundingAndTraining(this CalculationInputValues input)
        {
            if (input.PayBillGreaterThanThreeMillion)
            {
                var funding = Convert.ToInt32(input.TrainingCourse.MaxFundingAvailable * input.NumberRoles);
                return new CalculationOutputValues() { Funding = funding };
            }
            else if (!input.PayBillGreaterThanThreeMillion && (bool)input.OverFiftyEmployees)
            {
                var funding = Convert.ToInt32((input.TrainingCourse.MaxFundingAvailable * input.NumberRoles) * 0.95);
                var training = Convert.ToInt32((input.TrainingCourse.MaxFundingAvailable * input.NumberRoles) * 0.05);
                return new CalculationOutputValues() { Funding = funding, Training = training };
            }
            else if (!input.PayBillGreaterThanThreeMillion && !(bool)input.OverFiftyEmployees)
            {
                var funding = Convert.ToInt32(input.TrainingCourse.MaxFundingAvailable * input.NumberRoles);
                return new CalculationOutputValues() { Funding = funding };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
