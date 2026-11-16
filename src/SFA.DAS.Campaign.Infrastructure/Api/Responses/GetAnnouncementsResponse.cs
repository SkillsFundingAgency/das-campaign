using System.Collections.Generic;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Infrastructure.Api.Responses
{
    public class GetAnnouncementsResponse
    {
        [JsonProperty("announcements")]
        public AnnouncementsList Announcements { get; set; }
    }

    public class AnnouncementsList
    {
        [JsonProperty("announcements")]
        public List<AnnouncementSummary> Announcements { get; set; } = new List<AnnouncementSummary>();
    }
}
