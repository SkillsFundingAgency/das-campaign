namespace SFA.DAS.Campaign.Infrastructure.Configuration
{
    public class UserDataCryptography
    {
        public virtual string UserUrlSalt { get; set; }
        public virtual string AllowedUrlCharacters { get; set; }
        public virtual int UserUrlMinValue { get; set; }
    }
}
