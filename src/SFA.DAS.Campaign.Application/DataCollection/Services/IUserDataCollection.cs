using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.DataCollection;

namespace SFA.DAS.Campaign.Application.DataCollection
{
    public interface IUserDataCollection
    {
        Task StoreUserData(UserData userData);

        Task RemoveUserData(string email, bool receiveEmails);
    }
}
