using System.Web;
using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetAdvertsRequest : IGetApiRequest
    {
        private readonly string _postcode;
        private readonly uint _distance;
        private readonly string _route;

        public GetAdvertsRequest(string postcode, uint distance, string route)
        {
            _postcode = HttpUtility.UrlEncode(postcode);
            _distance = distance;
            _route = HttpUtility.UrlEncode(route);
        }

        public string GetUrl => $"adverts?postcode={_postcode}&route={_route}&distance={_distance}";
    }
}