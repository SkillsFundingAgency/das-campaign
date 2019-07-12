using System;
using System.Linq;
using System.Text;
using HashidsNet;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Application.Configuration;

namespace SFA.DAS.Campaign.Application.DataCollection
{
    public class UserDataCryptographyService : IUserDataCryptographyService
    {
        private readonly Hashids _hashedId;

        public UserDataCryptographyService(IOptions<UserDataCryptography> options)
        {
            _hashedId = new Hashids(options.Value.UserUrlSalt,options.Value.UserUrlMinValue,options.Value.AllowedUrlCharacters);
        }
        public string GenerateEncodedUserEmail(string email)
        {
            var hexEmail = string.Join("", email.Select(c => ((int)c).ToString("X2")));
            return _hashedId.EncodeHex(hexEmail);
        }
        public string DecodeUserEmail(string encodedUrl)
        {
            var decodedHex = _hashedId.DecodeHex(encodedUrl);
            return Encoding.ASCII.GetString(FromHex(decodedHex));    
        }
        public static byte[] FromHex(string hex)
        {
            var raw = new byte[hex.Length / 2];
            for (var i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
