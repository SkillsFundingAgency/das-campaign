using Microsoft.Extensions.Logging;
using SFA.DAS.Campaign.Application.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.DataCollection;

public class UserDataCollection(IUserDataCollectionValidator validator, ILogger<UserDataCollection> logger, IExternalApiService externalApiService) : IUserDataCollection
{
    public async Task StoreUserData(UserData userData)
    {
        var validationResult = validator.Validate(userData);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(new System.ComponentModel.DataAnnotations.ValidationResult("UserData model failed Validation", validationResult.Results), null, null);
        }

        try
        {
            logger.LogInformation("Registering Interest with UserData supplied.");

            var userDataDto = new UserDataDto
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                UkEmployerSize = userData.UkEmployerSize,
                PrimaryIndustry = userData.PrimaryIndustry,
                PrimaryLocation = userData.PrimaryLocation,
                AppsgovSignUpDate = userData.AppsgovSignUpDate,
                PersonOrigin = userData.PersonOrigin,
                IncludeInUR = userData.IncludeInUR
            };

            await externalApiService.PostDataAsync("RegisterInterest", userDataDto);

            logger.LogInformation("Successfully registered campaign interest.");
        }
        catch (System.Exception ex)
        {
            logger.LogError(ex, "An error occurred while logging validation success");
        }
    }
}
