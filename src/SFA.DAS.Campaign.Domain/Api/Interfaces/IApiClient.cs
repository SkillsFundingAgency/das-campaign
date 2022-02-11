using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Domain.Api.Interfaces
{
    public interface IApiClient
    {
        Task<TResponse> Get<TResponse>(IGetApiRequest request);
    }
}