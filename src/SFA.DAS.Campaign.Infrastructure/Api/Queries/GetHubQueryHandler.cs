using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
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
            var canPreview = _config.Value.AllowPreview;
            Page<Hub> hub = null;

            if (canPreview && request.Preview)
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
