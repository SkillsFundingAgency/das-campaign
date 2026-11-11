using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetAnnouncementRequest : IGetApiRequest
    {
        private readonly string _slug;

        public GetAnnouncementRequest(string slug)
        {
            _slug = slug;
        }

        public string GetUrl => $"announcements/{_slug}";
    }
}
