using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Application.DataCollection
{
    public class UserDataCollection : IUserDataCollection
    {
        private readonly IUserDataCollectionValidator _validator;
        private readonly IQueueService<UserData> _queueService;
        private readonly IOptions<UserDataQueueNames> _options;
        private readonly IUserDataCryptographyService _userDataCryptographyService;

        public UserDataCollection(IUserDataCollectionValidator validator, IQueueService<UserData> queueService,
            IOptions<UserDataQueueNames> options, IUserDataCryptographyService userDataCryptographyService)
        {
            _validator = validator;
            _queueService = queueService;
            _options = options;
            _userDataCryptographyService = userDataCryptographyService;
        }

        public async Task StoreUserData(UserData userData)
        {
            var validationResult = _validator.Validate(userData);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(new System.ComponentModel.DataAnnotations.ValidationResult("UserData model failed Validation", validationResult.Results), null, null);
            }

            userData.EncodedEmail = _userDataCryptographyService.GenerateEncodedUserEmail(userData.Email);

            await _queueService.AddMessageToQueue(userData, _options.Value.StoreUserDataQueueName);
        }
    }
}
