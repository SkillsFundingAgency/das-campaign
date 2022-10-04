using AutoFixture.NUnit3;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;
using System.Threading;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using FluentAssertions;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAPanel
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_The_Panel_Is_Returned_From_The_Preview_Api_if_Config_And_Param_Set(
            GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            SetupMockConfig(config);
            query.Preview = true;
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Slug}")), Times.Once);
            actual.Should().NotBeNull();
            actual.Panel.Should().NotBeNull();
        }

        [Test]
        [RecursiveMoqInlineAutoData(true)]
        [RecursiveMoqInlineAutoData(false)]
        public async Task Then_The_Api_Is_Called_With_The_Valid_Request_Parameters_And_Preview_Is_Disabled_Then_The_Panel_Is_Returned_From_The_Api(
            bool preview, GetPanelQuery query, Panel response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetPanelQueryHandler handler)
        {
            query.Preview = preview;
            SetupMockConfig(config, false);

            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(
                o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Slug}")), Times.Once);
            client.Verify(o => o.Get<Panel>(It.IsAny<GetPanelPreviewRequest>()), Times.Never);
            actual.Should().NotBeNull();
            actual.Panel.Should().NotBeNull();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_The_Api_Is_Called_With_Invalid_Request_Parameters_Then_No_Panel_Is_Returned(
             GetPanelQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetPanelQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Slug}"))).ReturnsAsync((Panel)null);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Slug}"))).ReturnsAsync((Panel)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Panel.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true)
        {
            config.Object.Value.AllowPreview = allowPreview;
        }
    }
}
