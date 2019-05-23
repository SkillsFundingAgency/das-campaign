using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Configuration
{
    public class AzureTableStorageConfigurationProvider : ConfigurationProvider
    {
        private readonly string _environment;
        private readonly string _version;
        private readonly CloudStorageAccount _storageAccount;

        public AzureTableStorageConfigurationProvider(string connection, string environment, string version)
        {
            _environment = environment;
            _version = version;
            _storageAccount = CloudStorageAccount.Parse(connection);
        }
        public override void Load()
        {
            var table = GetTable();
            var operation = GetOperation("SFA.DAS.Campaign", _environment, _version);
            var result = table.ExecuteAsync(operation).Result;

            var configItem = (ConfigurationItem)result.Result;

            using (var stream = configItem.Data.ToStream())
            {
                Data = JsonConfigurationParser.Parse(stream);
            }
        }

        private CloudTable GetTable()
        {
            var tableClient = _storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("Configuration");
        }
        private TableOperation GetOperation(string serviceName, string environmentName, string version)
        {
            return TableOperation.Retrieve<ConfigurationItem>(environmentName, $"{serviceName}_{version}");
        }
    }
}