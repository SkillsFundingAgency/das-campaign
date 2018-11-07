using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IUserDataCollectionValidator
    {
        bool Validate(UserData userData);
    }
}
