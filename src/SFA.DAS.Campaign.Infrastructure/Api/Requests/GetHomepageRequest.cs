using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetHomepageRequest : IGetApiRequest
    {
        public string GetUrl => "homepage";
    }
}
