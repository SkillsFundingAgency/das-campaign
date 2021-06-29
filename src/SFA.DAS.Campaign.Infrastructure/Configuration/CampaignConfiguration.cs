using SFA.DAS.Campaign.Application.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Configuration
{
    public class CampaignConfiguration
    {
        public  virtual string QueueConnectionString { get; set; }
        public virtual UserDataQueueNames UserDataQueueNames { get; set; }
        public virtual UserDataCryptography UserDataCryptography { get; set; }
        public virtual string FatBaseUrl { get ; set ; }
        public virtual OuterApiConfiguration OuterApi { get; set; }
        public virtual string EmployerAccountBaseUrl { get; set; }
    }

    public class OuterApiConfiguration
    {
        public string BaseUrl { get; set; }
        public string Key { get; set; }
        public bool AllowPreview { get;set; }
    }
}
