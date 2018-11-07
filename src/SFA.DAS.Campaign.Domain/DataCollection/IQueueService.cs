using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IQueueService<in T>
    {
        Task AddMessageToQueue(T message, string queueName);
    }
}
