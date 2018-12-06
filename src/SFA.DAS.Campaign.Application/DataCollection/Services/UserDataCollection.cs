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
        private readonly IUserDataCryptographyService _userDataCryptographyService;

        public UserDataCollection(IUserDataCollectionValidator validator, IQueueService<UserData> queueService,
            IOptions<CampaignConfiguration> options, IUserDataCryptographyService userDataCryptographyService)
        {
            _validator = validator;
            _queueService = queueService;
            _options = options;
            _userDataCryptographyService = userDataCryptographyService;
        }

        public async Task StoreUserData(UserData userData)
        {
            if (!_validator.Validate(userData))
            {
                throw new ArgumentException("UserData model failed validation", nameof(UserData));
            }

            userData.EncodedEmail = _userDataCryptographyService.GenerateEncodedUserEmail(userData.Email);

            await _queueService.AddMessageToQueue(userData, _options.Value.StoreUserDataQueueName);
        }

        public async Task RemoveUserData(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email must be supplied", nameof(email));
            }

            if (!_validator.ValidateEmail(email))
            {
                throw new ArgumentException("Email is not valid", nameof(email));
            }

            await _queueService.AddMessageToQueue(new UserData{Email = email}, _options.Value.RemoveUserDataQueueName);
        }
    }
}
