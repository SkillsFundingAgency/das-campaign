using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.FundingTool;
using SFA.DAS.Testing.AutoFixture;
using System;

namespace SFA.DAS.Campaign.UnitTests.Application.FundingCalculation
{
    public class WhenCalculatingFundingAvailable
    {
        [Test, MoqAutoData]
        public void AndEmployerIsLevy_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationInputValues calculationInput)
        {
            calculationInput.PayBillGreaterThanThreeMillion = true;

            var actual = calculationInput.CalculateFundingAndTraining();

            actual.Duration.Should().Be(calculationInput.TrainingCourse.Duration);
            actual.Level.Should().Be(calculationInput.TrainingCourse.Level);
            actual.Title.Should().Be(calculationInput.TrainingCourse.Title);
            actual.Funding.Should().Be(calculationInput.TrainingCourse.MaxFunding * calculationInput.NumberRoles);
            actual.Training.Should().Be(null);
        }

        [Test, MoqAutoData]
        public void AndEmployerIsNonLevyWithLessThanFiftyEmployees_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationInputValues calculationInput)
        {
            calculationInput.PayBillGreaterThanThreeMillion = false;
            calculationInput.OverFiftyEmployees = false;

            var actual = calculationInput.CalculateFundingAndTraining();

            actual.Duration.Should().Be(calculationInput.TrainingCourse.Duration);
            actual.Level.Should().Be(calculationInput.TrainingCourse.Level);
            actual.Title.Should().Be(calculationInput.TrainingCourse.Title);
            actual.Funding.Should().Be(calculationInput.TrainingCourse.MaxFunding * calculationInput.NumberRoles);
            actual.Training.Should().Be(null);
        }

        [Test, MoqAutoData]
        public void AndEmployerIsNonLevyFiftyOrMoreEmployees_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationInputValues calculationInput)
        {
            calculationInput.PayBillGreaterThanThreeMillion = false;
            calculationInput.OverFiftyEmployees = true;

            var actual = calculationInput.CalculateFundingAndTraining();

            actual.Duration.Should().Be(calculationInput.TrainingCourse.Duration);
            actual.Level.Should().Be(calculationInput.TrainingCourse.Level);
            actual.Title.Should().Be(calculationInput.TrainingCourse.Title);
            actual.Funding.Should().Be(Convert.ToInt32((calculationInput.TrainingCourse.MaxFunding * calculationInput.NumberRoles) * 0.95));
            actual.Training.Should().Be(Convert.ToInt32((calculationInput.TrainingCourse.MaxFunding * calculationInput.NumberRoles) * 0.05));
        }
    }
}
