using SFA.DAS.Campaign.Application.Configuration;

namespace SFA.DAS.Campaign.Models.Configuration
{
    public class CampaignConfiguration
    {
        public  string QueueConnectionString { get; set; }
        public  UserDataQueueNames UserDataQueueNames { get; set; }
        public  UserDataCryptography UserDataCryptography { get; set; }
        public  string FatBaseUrl { get ; set ; }
        public  OuterApiConfiguration OuterApi { get; set; }
        public  string EmployerAccountBaseUrl { get; set; }
    }

    public class OuterApiConfiguration
    {
        public string BaseUrl { get; set; }
        public string Key { get; set; }
    }
}
