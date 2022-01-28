using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Vacancies.Queries
{
    public class WhenGettingVacancies
    {
        [Test, MoqAutoData]
        public async Task Then_The_Api_Is_Called_And_Data_Returned(
            GetVacanciesQuery query,
            GetAdvertsResponse apiResponse,
            [Frozen] Mock<IApiClient> apiClient,
            GetVacanciesQueryHandler handler)
        {
            var expectedUrl = new GetAdvertsRequest(query.Postcode, query.Distance, query.Route);
            apiClient.Setup(x =>
                    x.Get<GetAdvertsResponse>(It.Is<GetAdvertsRequest>(c => c.GetUrl.Equals(expectedUrl.GetUrl))))
                .ReturnsAsync(apiResponse);

            var actual = await handler.Handle(query, CancellationToken.None);
            
            actual.Should().BeEquivalentTo(apiResponse);
        }
    }
}