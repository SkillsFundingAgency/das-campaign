using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Models.DataCollection;
using SFA.DAS.Campaign.Models.Validation;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IUserDataCollectionValidator
    {
        ValidationResult Validate(UserData userData);

        bool ValidateEmail(string email);
    }
}
