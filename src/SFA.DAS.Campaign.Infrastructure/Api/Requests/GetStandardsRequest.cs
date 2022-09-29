using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetStandardsRequest : IGetApiRequest
    {
        public string GetUrl => $"trainingcourses";
    }
}
