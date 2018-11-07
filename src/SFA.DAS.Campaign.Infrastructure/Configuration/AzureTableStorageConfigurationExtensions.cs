using Microsoft.Extensions.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Configuration
{
    public static class AzureTableStorageConfigurationExtensions
    {
        public static IConfigurationBuilder AddAzureTableStorageConfiguration(this IConfigurationBuilder builder, string connection, string environment, string version)
        {
            return builder.Add(new AzureTableStorageConfigurationSource(connection, environment, version));
        }
    }
}