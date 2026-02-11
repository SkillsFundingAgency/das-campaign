using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Services;

public interface IExternalApiService
{
    Task<string> PostDataAsync(string endpoint, object body);
}