using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.Configuration;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Application.DataCollection.Services
{
    public class UserDataCollection : IUserDataCollection
    {
        private readonly IUserDataCollectionValidator _validator;
        private readonly IQueueService<UserData> _queueService;
        private readonly IOptions<CampaignConfiguration> _options;
        
        public UserDataCollection(IUserDataCollectionValidator validator, IQueueService<UserData> queueService, IOptions<CampaignConfiguration> options)
        {
            _validator = validator;
            _queueService = queueService;
            _options = options;
        }

        public async Task StoreUserData(UserData userData)
        {
            if (!_validator.Validate(userData))
            {
                throw new ArgumentException("UserData model failed validation", nameof(UserData));
            }

            await _queueService.AddMessageToQueue(userData, _options.Value.StoreUserDataQueueName);
        }
    }
}
