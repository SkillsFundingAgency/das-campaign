using System;

namespace SFA.DAS.Campaign.Infrastructure.Models
{
    public class ApiApprenticeshipStandard
    {
        public int LarsCode { get; set; }
        public int TypicalDuration { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string Status { get; set; }
        public string Route { get; set; }
        public DateTime? ApprovedForDelivery { get; set; }
    }
}
