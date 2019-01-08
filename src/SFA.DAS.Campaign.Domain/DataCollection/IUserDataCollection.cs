using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Domain.DataCollection
{
    public interface IUserDataCollection
    {
        Task StoreUserData(UserData userData);

        Task RemoveUserData(string email, bool receiveEmails);
    }
}
