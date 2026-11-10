using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetAnnouncementPreviewRequest : IGetApiRequest
    {
        private readonly string _slug;

        public GetAnnouncementPreviewRequest(string slug)
        {
            _slug = slug;
        }

        public string GetUrl => $"announcements/preview/{_slug}";
    }
}
