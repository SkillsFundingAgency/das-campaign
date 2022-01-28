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
    public class WhenGettingAHub
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_The_Hub_Is_Returned_From_The_Preview_Api_if_Config_And_Param_Set(
            GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            SetupMockConfig(config);
            query.Preview = true;
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_Preview_Is_Disabled_Then_The_Hub_Is_Returned_From_The_Api(
            bool preview, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetHubQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);
            
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}")), Times.Once);
            client.Verify(o => o.Get<Page<Hub>>(It.IsAny<GetHubPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_Then_The_Hub_Is_Returned_From_The_Api_With_The_Menu(
            bool preview, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetHubQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);

            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}")), Times.Once);
            client.Verify(o => o.Get<Page<Hub>>(It.IsAny<GetHubPreviewRequest>()), Times.Never);

            actual.Should().NotBeNull();
            actual.Page.Should().NotBeNull();
            actual.Page.Content.Should().NotBeNull();
            actual.Page.Menu.Should().NotBeNull();
            actual.Page.Menu.Apprentices.Should().NotBeNullOrEmpty();
            actual.Page.Menu.Influencers.Should().NotBeNullOrEmpty();
            actual.Page.Menu.TopLevel.Should().NotBeNullOrEmpty();
            actual.Page.Menu.Employers.Should().NotBeNullOrEmpty();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_Then_No_Hub_Is_Returned(
            GetHubQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetHubQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}"))).ReturnsAsync((Page<Hub>)null);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync((Page<Hub>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true)
        {
            config.Object.Value.AllowPreview = allowPreview;
        }
    }
}
