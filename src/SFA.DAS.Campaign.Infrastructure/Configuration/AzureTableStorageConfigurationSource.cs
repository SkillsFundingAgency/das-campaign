using Microsoft.Extensions.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Configuration
{
    public class AzureTableStorageConfigurationSource : IConfigurationSource
    {
        private readonly string _connection;
        private readonly string _environment;
        private readonly string _version;

        public AzureTableStorageConfigurationSource(string connection, string environment, string version)
        {
            _connection = connection;
            _environment = environment;
            _version = version;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureTableStorageConfigurationProvider(_connection, _environment, _version);
        }
    }
}
