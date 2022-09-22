using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetPanelRequest : IGetApiRequest
    {
        private readonly string _slug;

        public GetPanelRequest(string slug)
        {
            _slug = slug;
        }

        public string GetUrl => $"panel?slug={_slug}";
    }
}
