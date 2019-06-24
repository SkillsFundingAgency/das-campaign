namespace SFA.DAS.Campaign.Application.DataCollection
{
    public class UserDataCollectionValidator : IUserDataCollectionValidator
    {
        public ValidationResult Validate(UserData userData)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(userData.FirstName))
            {
                validationResult.AddError(nameof(userData.FirstName),ValidationFailure.NotPopulated);
            }
            if (string.IsNullOrWhiteSpace(userData.LastName))
            {
                validationResult.AddError(nameof(userData.LastName), ValidationFailure.NotPopulated);
            }

            if (string.IsNullOrWhiteSpace(userData.Email))
            {
                validationResult.AddError(nameof(userData.Email), ValidationFailure.NotPopulated);
            }

            if (string.IsNullOrWhiteSpace(userData.CookieId))
            {
                validationResult.AddError(nameof(userData.CookieId), ValidationFailure.NotPopulated);
            }
            if(string.IsNullOrWhiteSpace(userData.RouteId))
            {
                validationResult.AddError(nameof(userData.RouteId), ValidationFailure.NotPopulated);
            }

            if (!userData.IsValidEmail())
            {
                validationResult.AddError(nameof(userData.Email), ValidationFailure.NotValid);
            }

            return validationResult;
        }

        public bool ValidateEmail(string email)
        {
            var userData = new UserData{Email = email};
            return userData.IsValidEmail();
        }
    }
}
