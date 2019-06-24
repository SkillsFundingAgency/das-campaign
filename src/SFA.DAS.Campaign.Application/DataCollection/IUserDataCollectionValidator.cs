namespace SFA.DAS.Campaign.Application.DataCollection
{
    public interface IUserDataCollectionValidator
    {
        ValidationResult Validate(UserData userData);

        bool ValidateEmail(string email);
    }
}
