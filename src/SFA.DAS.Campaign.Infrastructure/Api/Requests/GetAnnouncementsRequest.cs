using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetAnnouncementsRequest : IGetApiRequest
    {
        public string GetUrl => "announcements";
    }
}
