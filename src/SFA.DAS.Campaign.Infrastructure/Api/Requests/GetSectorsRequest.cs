using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetSectorsRequest : IGetApiRequest
    {
        public string GetUrl => "sectors";
    }
}