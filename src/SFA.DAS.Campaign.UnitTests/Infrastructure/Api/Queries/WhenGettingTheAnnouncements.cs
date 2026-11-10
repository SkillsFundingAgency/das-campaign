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
    public class WhenGettingTheAnnouncements
    {
        [Test]
        [MoqInlineAutoData(true, true)]
        [MoqInlineAutoData(false, false)]
        public async Task AndForcePreviewIsTrue_ThenTheAnnouncementsAreReturnedFromThePreviewAPI(
            bool allowPreview, bool previewWanted, GetAnnouncementsQuery query, GetAnnouncementsResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementsQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, true);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsPreviewRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsRequest>()), Times.Never);
            actual.Announcements.Should().BeSameAs(response.Announcements.Announcements);
        }

        [Test, MoqAutoData]
        public async Task AndPreviewIsAllowedAndWanted_ThenTheAnnouncementsAreReturnedFromThePreviewAPI(
            GetAnnouncementsQuery query, GetAnnouncementsResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementsQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsPreviewRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsRequest>()), Times.Never);
            actual.Announcements.Should().BeSameAs(response.Announcements.Announcements);
        }

        [Test]
        [MoqInlineAutoData(false, true)]
        [MoqInlineAutoData(true, false)]
        public async Task AndPreviewIsNotAllowedOrNotWanted_ThenTheAnnouncementsAreReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetAnnouncementsQuery query, GetAnnouncementsResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementsQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsPreviewRequest>()), Times.Never);
            actual.Announcements.Should().BeSameAs(response.Announcements.Announcements);
        }

        [Test, MoqAutoData]
        public async Task AndThePreviewAPIReturnsNothing_ThenTheProductionAPIIsUsed(
            GetAnnouncementsQuery query, GetAnnouncementsResponse response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementsQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsPreviewRequest>())).ReturnsAsync((GetAnnouncementsResponse)null);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Announcements.Should().BeSameAs(response.Announcements.Announcements);
        }

        [Test, MoqAutoData]
        public async Task AndTheAPIReturnsNothing_ThenAnEmptyListIsReturned(
            GetAnnouncementsQuery query,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementsQueryHandler handler)
        {
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<GetAnnouncementsResponse>(It.IsAny<GetAnnouncementsRequest>())).ReturnsAsync((GetAnnouncementsResponse)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Announcements.Should().NotBeNull().And.BeEmpty();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview, bool forcePreview)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}
