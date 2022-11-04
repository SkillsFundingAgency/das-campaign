using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Testing.AutoFixture;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.UnitTests.Application.GetStandard
{
    public class WhenGettingStandard
    {
        [Test, MoqAutoData]
        public async Task ThenTheCorrectStandardIsReturned( GetStandardQuery request, StandardResponse standard,  
            CancellationToken cancellationToken, [Frozen] Mock<IStandardsRepository> mockStandardsRepository)
        {
            standard.StandardUId = request.StandardUId;
            mockStandardsRepository.Setup(x => x.GetStandard(request.StandardUId)).ReturnsAsync(standard);

            var handler = new GetStandardQueryHandler(mockStandardsRepository.Object);
            var actual = await handler.Handle(request, cancellationToken);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<GetStandardQueryResult>();
            actual.Title.Should().BeEquivalentTo(standard.Title);
            actual.Level.Should().Be(standard.Level);
            actual.StandardUId.Should().BeEquivalentTo(request.StandardUId);
            actual.LarsCode.Should().Be(standard.LarsCode);
        }
    }
}
