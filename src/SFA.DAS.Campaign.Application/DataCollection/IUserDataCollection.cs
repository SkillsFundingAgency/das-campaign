using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.DataCollection
{
    public interface IUserDataCollection
    {
        Task StoreUserData(UserData userData);
    }
}
