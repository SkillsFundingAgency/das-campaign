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
    public class WhenGettingAnAnnouncement
    {
        [Test, RecursiveMoqAutoData]
        public async Task AndForcePreviewIsTrue_ThenTheAnnouncementIsReturnedFromThePreviewAPI(
            GetAnnouncementQuery query, Announcement response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementQueryHandler handler)
        {
            query.Preview = false;
            SetupMockConfig(config, false, true);
            client.Setup(o => o.Get<Announcement>(It.Is<GetAnnouncementPreviewRequest>(r => r.GetUrl == $"announcements/preview/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Announcement>(It.IsAny<GetAnnouncementRequest>()), Times.Never);
            actual.Announcement.Should().BeSameAs(response);
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndPreviewIsAllowedAndWanted_ThenTheAnnouncementIsReturnedFromThePreviewAPI(
            GetAnnouncementQuery query, Announcement response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Announcement>(It.Is<GetAnnouncementPreviewRequest>(r => r.GetUrl == $"announcements/preview/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Announcement>(It.IsAny<GetAnnouncementRequest>()), Times.Never);
            actual.Announcement.Should().BeSameAs(response);
        }

        [Test]
        [RecursiveMoqInlineAutoData(false, true)]
        [RecursiveMoqInlineAutoData(true, false)]
        public async Task AndPreviewIsNotAllowedOrNotWanted_ThenTheAnnouncementIsReturnedFromTheProductionAPI(
            bool allowPreview, bool previewWanted, GetAnnouncementQuery query, Announcement response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementQueryHandler handler)
        {
            query.Preview = previewWanted;
            SetupMockConfig(config, allowPreview, false);
            client.Setup(o => o.Get<Announcement>(It.Is<GetAnnouncementRequest>(r => r.GetUrl == $"announcements/{query.Slug}"))).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            client.Verify(o => o.Get<Announcement>(It.IsAny<GetAnnouncementPreviewRequest>()), Times.Never);
            actual.Announcement.Should().BeSameAs(response);
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndThePreviewAPIReturnsNothing_ThenTheProductionAPIIsUsed(
            GetAnnouncementQuery query, Announcement response,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementQueryHandler handler)
        {
            query.Preview = true;
            SetupMockConfig(config, true, false);
            client.Setup(o => o.Get<Announcement>(It.IsAny<GetAnnouncementPreviewRequest>())).ReturnsAsync((Announcement)null);
            client.Setup(o => o.Get<Announcement>(It.IsAny<GetAnnouncementRequest>())).ReturnsAsync(response);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Announcement.Should().BeSameAs(response);
        }

        [Test, RecursiveMoqAutoData]
        public async Task AndTheAnnouncementDoesNotExist_ThenNullIsReturned(
            GetAnnouncementQuery query,
            [Frozen] Mock<IOptions<CampaignConfiguration>> config, [Frozen] Mock<IApiClient> client, GetAnnouncementQueryHandler handler)
        {
            SetupMockConfig(config, false, false);
            client.Setup(o => o.Get<Announcement>(It.IsAny<GetAnnouncementRequest>())).ReturnsAsync((Announcement)null);

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
            actual.Announcement.Should().BeNull();
        }

        private static void SetupMockConfig(Mock<IOptions<CampaignConfiguration>> config, bool allowPreview, bool forcePreview)
        {
            config.Object.Value.AllowPreview = allowPreview;
            config.Object.Value.ForcePreview = forcePreview;
        }
    }
}
