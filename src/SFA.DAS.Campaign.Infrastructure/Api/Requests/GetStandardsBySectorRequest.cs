using System.Web;
using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetStandardsBySectorRequest : IGetApiRequest
    {
        private readonly string _sector;

        public GetStandardsBySectorRequest(string sector)
        {
            _sector = HttpUtility.UrlEncode(sector);
        }

        public string GetUrl => $"trainingcourses?sector={_sector}";
    }
}