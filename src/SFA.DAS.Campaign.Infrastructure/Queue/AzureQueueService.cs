using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Application.Core;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Queue
{
    public class AzureQueueService<T> : IQueueService<T>
    {
        private readonly CloudQueueClient _queueClient;

        public AzureQueueService(IOptions<CampaignConfiguration> configuration)
        {
            var storageAccount = CloudStorageAccount.Parse(configuration.Value.QueueConnectionString);
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task AddMessageToQueue(T message, string queueName)
        {
            var queue = _queueClient.GetQueueReference(queueName);
            
            await queue.CreateIfNotExistsAsync();

            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));
        }
    }
}
