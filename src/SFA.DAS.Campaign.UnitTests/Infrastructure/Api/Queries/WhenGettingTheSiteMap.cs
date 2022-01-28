using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingTheSiteMap
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_And_The_SiteMap_Is_Returned_From_The_Api(
            GetSiteMapQuery query, Page<SiteMap> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetSiteMapQueryHandler handler)
        {
            client.Setup(o => o.Get<Page<SiteMap>>(It.Is<GetSiteMapRequest>(r => r.GetUrl == $"sitemap"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<SiteMap>>(It.Is<GetSiteMapRequest>(r => r.GetUrl == $"sitemap")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }
        
    }
}
