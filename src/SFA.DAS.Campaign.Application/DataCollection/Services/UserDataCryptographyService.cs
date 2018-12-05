using System;
using HashidsNet;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Models.Configuration;

namespace SFA.DAS.Campaign.Application.DataCollection.Services
{
    public class UserDataCryptographyService
    {
        private readonly Hashids _hashedId;

        public UserDataCryptographyService(IOptions<CampaignConfiguration> options)
        {
            _hashedId = new Hashids(options.Value.UserUrlSalt,options.Value.UserUrlMinValue,options.Value.AllowedUrlCharacters);
        }
        public string GenerateEncodedUserId(int userId)
        {
            return _hashedId.Encode(userId);
        }

        public long DecodeUserId(string encodedUrl)
        {
            try
            {
                return _hashedId.Decode(encodedUrl)[0];
            }
            catch (IndexOutOfRangeException e)
            {

                return 0;
            }
            
        }
    }
}
