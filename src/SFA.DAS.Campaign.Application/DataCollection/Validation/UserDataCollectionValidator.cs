using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Domain.DataCollection;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Application.DataCollection.Validation
{
    public class UserDataCollectionValidator : IUserDataCollectionValidator
    {
        public bool Validate(UserData userData)
        {
            if (string.IsNullOrWhiteSpace(userData.FirstName) ||
                string.IsNullOrWhiteSpace(userData.LastName) ||
                string.IsNullOrWhiteSpace(userData.Email) ||
                string.IsNullOrWhiteSpace(userData.CookieId) ||
                string.IsNullOrWhiteSpace(userData.RouteId))
            {
                return false;
            }

                return true;
        }
    }
}
