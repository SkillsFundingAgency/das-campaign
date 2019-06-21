using SFA.DAS.Campaign.Application.Configuration;

namespace SFA.DAS.Campaign.Models.Configuration
{
    public class CampaignConfiguration
    {
        
        public virtual string QueueConnectionString { get; set; }
        public virtual UserDataQueueNames UserDataQueueNames { get; set; }
        public virtual UserDataCryptography UserDataCryptography { get; set; }

    }
}
