using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IQueueService<in T>
    {
        Task AddMessageToQueue(T message, string queueName);
    }
}
