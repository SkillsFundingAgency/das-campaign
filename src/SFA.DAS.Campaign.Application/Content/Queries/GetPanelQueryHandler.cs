using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetPanelQueryHandler : IRequestHandler<GetPanelQuery, GetPanelQueryResult<Panel>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetPanelQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;  
        }

        public async Task<GetPanelQueryResult<Panel>> Handle(GetPanelQuery request, CancellationToken cancellationToken)
        {
            var canPreview = _config.Value.AllowPreview;
            Page<Panel> panel = null;

            if (canPreview && request.Preview)
            {
                panel = await _apiClient.Get<Page<Panel>>(new GetPanelPreviewRequest(request.Slug)).ConfigureAwait(false);
            }

            if (panel == null)
            {
                panel = await _apiClient.Get<Page<Panel>>(new GetPanelRequest(request.Slug)).ConfigureAwait(false);
            }

            return new GetPanelQueryResult<Panel>
            {
                Page = panel
            };
        }
    }
}
