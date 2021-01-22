using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsRepository _standardsRepository;
        private GetSectorsResponse _sectorResponse;
        private GetStandardsResponse _standardsResponse;
        private string _sector;

        [SetUp]
        public void Arrange()
        {
            var fixture = new Fixture();
            _sectorResponse = fixture.Create<GetSectorsResponse>();
            _standardsResponse = fixture.Create<GetStandardsResponse>();
            _sector = fixture.Create<string>();
            var apiClient = new Mock<IApiClient>();
            apiClient.Setup(x => x.Get<GetSectorsResponse>(It.IsAny<GetSectorsRequest>()))
                .ReturnsAsync(_sectorResponse);
            apiClient.Setup(x => x.Get<GetStandardsResponse>(It.Is<GetStandardsBySectorRequest>(c=>c.GetUrl.Contains(_sector))))
                .ReturnsAsync(_standardsResponse);
           
            _standardsRepository = new StandardsRepository(apiClient.Object);
        }

        [Test]
        public async Task Then_The_Sectors_Are_Retrieved_From_The_Api()
        {
            //Act
            var actual = await _standardsRepository.GetRoutes();
            
            //Assert
            actual.Should().BeEquivalentTo(_sectorResponse.Sectors.Select(c=>c.Route).ToList());
        }

        [Test]
        public async Task Then_The_Standards_Are_Returned_By_Sector()
        {
            //Act
            var actual = await _standardsRepository.GetByRoute(_sector);
            
            //Assert
            actual.Should().BeEquivalentTo(_standardsResponse.Standards.Select(c=>c.Id).ToList());
        }
    }
}
