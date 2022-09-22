using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetPanelPreviewRequest : IGetApiRequest
    {
        private readonly string _slug;

        public GetPanelPreviewRequest(string slug)
        {
            _slug = slug;
        }

        public string GetUrl => $"panel/preview/{_slug}";
    }
}
