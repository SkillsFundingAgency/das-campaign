using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.Campaign.Application.Content.Queries;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Queries
{
    public class WhenGettingTheHomepage
    {
        [Test]
        [MoqInlineAutoData(true, true)]
        [MoqInlineAutoData(true, false)]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsTrue_ThenTheHomepageIsReturnedFromThePreviewAPI(
            bool allowPreview, bool previewWanted, GetHomepageQuery query, GetHomepageResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHomepageQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, true);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>()), Times.Once);
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>()), Times.Never);
                actual.Homepage.Should().BeSameAs(response.Homepage);
            });
        }

        [Test, MoqAutoData]
        public async Task AndForcePreviewIsFalse_AndAllowPreviewIsTrue_AndPreviewIsWanted_ThenTheHomepageIsReturnedFromThePreviewAPI(
            GetHomepageQuery query, GetHomepageResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHomepageQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>()), Times.Once);
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>()), Times.Never);
                actual.Homepage.Should().BeSameAs(response.Homepage);
            });
        }

        [Test]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(true, false)]
        [MoqInlineAutoData(false, false)]
        public async Task AndPreviewIsNotAllowedOrNotWanted_ThenTheHomepageIsReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetHomepageQuery query, GetHomepageResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHomepageQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>()), Times.Once);
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>()), Times.Never);
                actual.Homepage.Should().BeSameAs(response.Homepage);
            });
        }

        [Test, MoqAutoData]
        public async Task AndThePreviewAPIReturnsNothing_ThenTheProductionAPIIsUsedAsAFallback(
            GetHomepageQuery query, GetHomepageResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHomepageQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>())).ReturnsAsync((GetHomepageResponse)null);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepagePreviewRequest>()), Times.Once);
                client.Verify(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>()), Times.Once);
                actual.Homepage.Should().BeSameAs(response.Homepage);
            });
        }

        [Test, MoqAutoData]
        public async Task AndTheAPIReturnsNothing_ThenNoHomepageIsReturned(
            GetHomepageQuery query,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetHomepageQueryHandler handler)
        {
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<GetHomepageResponse>(It.IsAny<GetHomepageRequest>())).ReturnsAsync((GetHomepageResponse)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Homepage.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview, bool forcePreview)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}
