using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IUserDataCryptographyService
    {
        string GenerateEncodedUserEmail(string email);
    }
}
