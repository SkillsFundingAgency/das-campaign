using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetPanelPreviewRequest : IGetApiRequest
    {
        private readonly int _panelId;

        public GetPanelPreviewRequest(int id)
        {
            _panelId = id;
        }

        public string GetUrl => $"panel/preview/{_panelId}";
    }
}
