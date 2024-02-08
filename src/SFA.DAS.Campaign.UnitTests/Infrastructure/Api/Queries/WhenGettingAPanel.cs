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
        [Test]
        [MoqInlineAutoData(true, true)]
        [MoqInlineAutoData(true, false)]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsTrue_ThenThePanelIsReturnedFromThePreviewAPI(
            bool allowPreview, bool previewWanted, GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, true);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Id}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Id}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Panel.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsWanted_ThenThePanelIsReturnedFromThePreviewAPI(
           GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Id}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Id}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Panel.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}")), Times.Once);
                client.Verify(o => o.Get<Panel>(It.IsAny<GetPanelPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Panel.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsNotWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            query.Preview = false;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}")), Times.Once);
                client.Verify(o => o.Get<Panel>(It.IsAny<GetPanelPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Panel.Should().NotBeNull();
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsNotWanted_ThenThePanelIsReturnedFromTheProductionAPI(
            GetPanelQuery query, Panel response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetPanelQueryHandler handler)
        {
            query.Preview = false;
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}")), Times.Once);
                client.Verify(o => o.Get<Panel>(It.IsAny<GetPanelPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Panel.Should().NotBeNull();
            });
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndTheApiIsCalledWithInvalidRequestParameters_ThenNoPanelIsReturned(
             GetPanelQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetPanelQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelPreviewRequest>(r => r.GetUrl == $"panel/preview/{query.Id}"))).ReturnsAsync((Panel)null);
            client.Setup(o => o.Get<Panel>(It.Is<GetPanelRequest>(r => r.GetUrl == $"panel/{query.Id}"))).ReturnsAsync((Panel)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Panel.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true, bool forcePreview = false)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}
