namespace SFA.DAS.Campaign.Application.DataCollection
{
    public interface IUserDataCryptographyService
    {
        string GenerateEncodedUserEmail(string email);
        string DecodeUserEmail(string encodedUrl);
    }
}
