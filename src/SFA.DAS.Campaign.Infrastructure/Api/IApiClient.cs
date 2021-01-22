using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.Api
{
    public interface IApiClient
    {
        Task<TResponse> Get<TResponse>(IGetApiRequest request);
    }
}