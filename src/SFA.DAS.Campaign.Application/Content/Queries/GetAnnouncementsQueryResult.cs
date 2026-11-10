using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetAnnouncementsQueryResult
    {
        public List<AnnouncementSummary> Announcements { get; set; } = new List<AnnouncementSummary>();
    }
}
