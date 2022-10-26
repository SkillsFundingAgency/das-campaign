using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Threading;

namespace SFA.DAS.Campaign.UnitTests.Application.ApprenticeshipBenefitsAndFunding
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
            TestFormatting(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles), actual.Result.Format(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles)));
            TestFormatting(0, actual.Result.Format(0));
        }

        [Test, MoqAutoData]
        public void AndEmployerIsNonLevy_ThenTheCorrectFundingAndTrainingCostsAreCalculated(CalculationQuery query, CancellationToken cancellationToken, CalculationQueryHandler handler)
        {
            query.PayBillGreaterThanThreeMillion = false;

            var actual = handler.Handle(query, cancellationToken);

            actual.Result.Duration.Should().Be(query.TrainingCourse.Duration);
            actual.Result.Level.Should().Be(query.TrainingCourse.Level);
            actual.Result.Title.Should().Be(query.TrainingCourse.Title);
            actual.Result.Funding.Should().Be(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.95));
            actual.Result.Training.Should().Be(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.05));
            TestFormatting(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.95), actual.Result.Format(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.95)));
            TestFormatting(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.05), actual.Result.Format(Convert.ToInt32(query.TrainingCourse.MaxFunding * query.NumberRoles * 0.05)));
        }

        public AndConstraint<FluentAssertions.Primitives.StringAssertions> TestFormatting(int? num, string numOutput)
        {
            if (num == null) { return numOutput.Should().Be(null); }
            switch (num.ToString().Length)
            {
                case 4:
                    return numOutput.Should().Be(num.ToString().Insert(1, ","));
                case 5:
                    return numOutput.Should().Be(num.ToString().Insert(2, ","));
                case 6:
                    return numOutput.Should().Be(num.ToString().Insert(3, ","));
                case 7:
                    var expected = num.ToString();
                    expected = expected.Insert(1, ",");
                    expected = expected.Insert(5, ",");
                    return numOutput.Should().Be(expected);
                default:
                    return numOutput.Should().Be(num.Value.ToString());
            }
        }
    }
}
