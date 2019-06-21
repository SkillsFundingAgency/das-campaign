
using Microsoft.Azure.Cosmos.Table;

namespace SFA.DAS.Campaign.Models.Configuration
{
    public class ConfigurationItem : TableEntity
    {
        public string Data { get; set; }
    }
}
