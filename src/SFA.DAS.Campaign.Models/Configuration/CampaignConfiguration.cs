namespace SFA.DAS.Campaign.Models.Configuration
{
    public class CampaignConfiguration
    {
        public virtual string StoreUserDataQueueName { get; set; }
        public virtual string RemoveUserDataQueueName { get; set; }
        public virtual string QueueConnectionString { get; set; }
        public virtual string UserUrlSalt { get; set; }
        public virtual string AllowedUrlCharacters { get; set; }
        public virtual int UserUrlMinValue { get; set; }
    }
}
