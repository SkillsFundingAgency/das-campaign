using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetHubQueryHandler : IRequestHandler<GetHubQuery, GetHubQueryResult<Hub>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetHubQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetHubQueryResult<Hub>> Handle(GetHubQuery request, CancellationToken cancellationToken)
        {
            var allowPreview = _config.Value.AllowPreview;
            var forcePreview = _config.Value.ForcePreview;
            Page<Hub> hub = null;

            if (allowPreview && request.Preview || forcePreview)
            {
                hub = await _apiClient.Get<Page<Hub>>(new GetHubPreviewRequest(request.Hub)).ConfigureAwait(false);
            }
            
            if (hub == null)
            {
                hub = await _apiClient.Get<Page<Hub>>(new GetHubRequest(request.Hub)).ConfigureAwait(false);
            }

            return new GetHubQueryResult<Hub>
            {
                Page = hub
            };
        }
    }
}
