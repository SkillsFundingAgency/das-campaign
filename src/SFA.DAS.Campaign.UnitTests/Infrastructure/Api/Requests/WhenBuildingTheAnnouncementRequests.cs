using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.UnitTests.Infrastructure.Api.Requests
{
    public class WhenBuildingTheAnnouncementRequests
    {
        [Test]
        public void Then_The_Announcements_Url_Is_Correct()
        {
            new GetAnnouncementsRequest().GetUrl.Should().Be("announcements");
        }

        [Test]
        public void Then_The_Announcements_Preview_Url_Is_Correct()
        {
            new GetAnnouncementsPreviewRequest().GetUrl.Should().Be("announcements/preview");
        }

        [Test]
        public void Then_The_Announcement_Url_Is_Correct()
        {
            new GetAnnouncementRequest("my-slug").GetUrl.Should().Be("announcements/my-slug");
        }

        [Test]
        public void Then_The_Announcement_Preview_Url_Is_Correct()
        {
            new GetAnnouncementPreviewRequest("my-slug").GetUrl.Should().Be("announcements/preview/my-slug");
        }
    }
}
