using SFA.DAS.Campaign.Domain.Api.Interfaces;
using System.Web;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetStandardRequest : IGetApiRequest
    {
        private readonly string _standardUId;

        public GetStandardRequest(string standardUId)
        {
            _standardUId = HttpUtility.UrlEncode(standardUId);
        }
        public string GetUrl => $"trainingcourses/{_standardUId}";
    }
}
