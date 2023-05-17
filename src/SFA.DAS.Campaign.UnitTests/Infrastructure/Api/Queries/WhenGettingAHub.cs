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
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingAHub
    {
        [Test]
        [MoqInlineAutoData(true, true)]
        [MoqInlineAutoData(true, false)]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsTrue_ThenTheHubIsReturnedFromThePreviewAPI(
            bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, true);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqInlineAutoData(true, true)]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsWanted_ThenTheHubIsReturnedFromThePreviewAPI(
           bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}")), Times.Once);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqInlineAutoData(false, true)]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsWanted_ThenTheHubIsReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}")), Times.Once);
                client.Verify(o => o.Get<Page<Hub>>(It.IsAny<GetHubPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqInlineAutoData(true, false)]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsNotWanted_ThenTheHubIsReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}")), Times.Once);
                client.Verify(o => o.Get<Page<Hub>>(It.IsAny<GetHubPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test, MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsFalse_AndPreviewIsNotWanted_ThenTheHubIsReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}")), Times.Once);
                client.Verify(o => o.Get<Page<Hub>>(It.IsAny<GetPanelPreviewRequest>()), Times.Never);
                actual.Should().NotBeNull();
                actual.Page.Should().NotBeNull();
            });
        }

        [Test]
        [RecursiveMoqInlineAutoData(true, false)]
        [RecursiveMoqInlineAutoData(false, false)]
        public async Task AndTheApiIsCalledWithTheValidRequestParameters_ThenTheHubIsReturnedFromTheProductionApiWithTheMenu(
            bool allowPreview, bool previewWanted, GetHubQuery query, Page<Hub> response, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetHubQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);

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
        public async Task AndTheApiIsCalledWithInvalidRequestParameters_ThenNoHubIsReturned(
            GetHubQuery query, [Frozen] Mock<IApiClient> client, [Frozen] Mock<IOptions<CampaignConfiguration>> config, GetHubQueryHandler handler)
        {
            SetupMockConfig(config);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubPreviewRequest>(r => r.GetUrl == $"hub/preview/{query.Hub}"))).ReturnsAsync((Page<Hub>)null);
            client.Setup(o => o.Get<Page<Hub>>(It.Is<GetHubRequest>(r => r.GetUrl == $"hub/{query.Hub}"))).ReturnsAsync((Page<Hub>)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Page.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview = true, bool forcePreview = false)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}
