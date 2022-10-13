using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading;

namespace SFA.DAS.Campaign.UnitTests.Application.FundingCalculation
{
    public class WhenCalculatingFundingAvailable
    { 
        [Test, MoqAutoData]
        public void AndEmployerIsLevy_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationQuery query, CancellationToken cancellationToken, CalculationQueryHandler handler)
        {
            query.PayBillGreaterThanThreeMillion = true;

            var actual = handler.Handle(query, cancellationToken);

            actual.Result.Duration.Should().Be(query.TrainingCourse.Duration);
            actual.Result.Level.Should().Be(query.TrainingCourse.Level);
            actual.Result.Title.Should().Be(query.TrainingCourse.Title);
            actual.Result.Funding.Should().Be(query.TrainingCourse.MaxFunding * query.NumberRoles);
            actual.Result.Training.Should().Be(0);
        }

        [Test, MoqAutoData]
        public void AndEmployerIsNonLevyWithLessThanFiftyEmployees_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationQuery query, CancellationToken cancellationToken, CalculationQueryHandler handler)
        {
            query.PayBillGreaterThanThreeMillion = false;
            query.OverFiftyEmployees = false;

            var actual = handler.Handle(query, cancellationToken);

            actual.Result.Duration.Should().Be(query.TrainingCourse.Duration);
            actual.Result.Level.Should().Be(query.TrainingCourse.Level);
            actual.Result.Title.Should().Be(query.TrainingCourse.Title);
            actual.Result.Funding.Should().Be(query.TrainingCourse.MaxFunding * query.NumberRoles);
            actual.Result.Training.Should().Be(0);
        }

        [Test, MoqAutoData]
        public void AndEmployerIsNonLevyFiftyOrMoreEmployees_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationQuery query, CancellationToken cancellationToken, CalculationQueryHandler handler)
        {
            query.PayBillGreaterThanThreeMillion = false;
            query.OverFiftyEmployees = true;

            var actual = handler.Handle(query, cancellationToken);

            actual.Result.Duration.Should().Be(query.TrainingCourse.Duration);
            actual.Result.Level.Should().Be(query.TrainingCourse.Level);
            actual.Result.Title.Should().Be(query.TrainingCourse.Title);
            actual.Result.Funding.Should().Be(Convert.ToInt32((query.TrainingCourse.MaxFunding * query.NumberRoles) * 0.95));
            actual.Result.Training.Should().Be(Convert.ToInt32((query.TrainingCourse.MaxFunding * query.NumberRoles) * 0.05));
        }
    }
}
