using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandards;
using FluentAssertions;

namespace SFA.DAS.Campaign.UnitTests.Application.GetStandards
{
    public class WhenGettingStandards
    {
        [Test, MoqAutoData]
        public async Task ThenTheCorrectStandardsAreReturned(GetStandardsQuery request, List<StandardResponse> standards,
            CancellationToken cancellationToken, [Frozen] Mock<IStandardsRepository> mockStandardsRepository)
        {
            mockStandardsRepository.Setup(x => x.GetStandards(null)).ReturnsAsync(standards);

            var handler = new GetStandardsQueryHandler(mockStandardsRepository.Object);
            var actual = await handler.Handle(request, cancellationToken);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<GetStandardsQueryResult>();
            actual.Standards.Should().HaveCount(standards.Count);
        }
    }
}
