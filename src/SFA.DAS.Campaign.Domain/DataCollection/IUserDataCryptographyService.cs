namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IUserDataCryptographyService
    {
        string GenerateEncodedUserEmail(string email);
        string DecodeUserEmail(string encodedUrl);
    }
}
