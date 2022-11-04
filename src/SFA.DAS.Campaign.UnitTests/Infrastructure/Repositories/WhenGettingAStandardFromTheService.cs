using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    public class WhenGettingAStandardFromTheService
    {
        private StandardsRepository _standardsRepository;
        private GetSectorsResponse _sectorResponse;
        private GetStandardsResponse _standardsResponse;
        private GetStandardResponse _standardResponse;
        private string _sector;
        private string _standardUId;


        [SetUp]
        public void Arrange()
        {
            var fixture = new Fixture();
            _standardUId = fixture.Create<string>();
            _sectorResponse = fixture.Create<GetSectorsResponse>();
            _standardsResponse = fixture.Create<GetStandardsResponse>();
            _standardResponse = fixture.Create<GetStandardResponse>();
            _standardResponse.StandardUId = _standardUId;
            _sector = fixture.Create<string>();
            var apiClient = new Mock<IApiClient>();
            apiClient.Setup(x => x.Get<GetSectorsResponse>(It.IsAny<GetSectorsRequest>()))
                .ReturnsAsync(_sectorResponse);
            apiClient.Setup(x => x.Get<GetStandardsResponse>(It.Is<GetStandardsBySectorRequest>(c=>c.GetUrl.Contains(_sector))))
                .ReturnsAsync(_standardsResponse);
            apiClient.Setup(x => x.Get<GetStandardsResponse>(It.Is<GetStandardsBySectorRequest>(c => c.GetUrl.Equals("trainingcourses"))))
                .ReturnsAsync(_standardsResponse);
            apiClient.Setup(x => x.Get<GetStandardResponse>(It.Is<GetStandardRequest>(c=> c.GetUrl.Contains(_standardUId))))
                .ReturnsAsync(_standardResponse);

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
        public async Task AndThereIsASector_ThenTheStandardsAreRecievedFromTheApi()
        {
            var actual = await _standardsRepository.GetStandards(_sector);
            
            actual.Should().BeEquivalentTo(_standardsResponse.Standards.Select(c=> new {c.Title, c.LarsCode, c.StandardUId, c.Level }).ToList());
        }

        [Test]
        public async Task AndThereIsNoSector_ThenTheStandardsAreRecievedFromTheApi()
        {
            var actual = await _standardsRepository.GetStandards(null);

            actual.Should().BeEquivalentTo(_standardsResponse.Standards.Select(c => new { c.Title, c.LarsCode, c.StandardUId, c.Level }).ToList());
        }

        [Test]
        public async Task ThenTheStandardIsRecievedFromTheApi()
        {
            var actual = await _standardsRepository.GetStandard(_standardUId);

            actual.StandardUId.Should().BeEquivalentTo(_standardUId);
        }
    }
}
