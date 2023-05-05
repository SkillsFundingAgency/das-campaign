using SFA.DAS.Campaign.Domain.Api.Interfaces;

namespace SFA.DAS.Campaign.Infrastructure.Api.Requests
{
    public class GetPanelRequest : IGetApiRequest
    {
        private readonly int _panelId;

        public GetPanelRequest(int id)
        {
            _panelId = id;
        }

        public string GetUrl => $"panel/{_panelId}";
    }
}
