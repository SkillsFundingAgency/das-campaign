using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Core
{
    public interface IQueueService<in T>
    {
        Task AddMessageToQueue(T message, string queueName);
    }
}
