using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Api.Responses;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetHomepageQueryHandler : IRequestHandler<GetHomepageQuery, GetHomepageQueryResult>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetHomepageQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetHomepageQueryResult> Handle(GetHomepageQuery request, CancellationToken cancellationToken)
        {
            var allowPreview = _config.Value.AllowPreview;
            var forcePreview = _config.Value.ForcePreview;
            Homepage homepage = null;

            if ((allowPreview && request.Preview) || forcePreview)
            {
                var preview = await _apiClient.Get<GetHomepageResponse>(new GetHomepagePreviewRequest()).ConfigureAwait(false);
                homepage = preview?.Homepage;
            }

            if (homepage == null)
            {
                var response = await _apiClient.Get<GetHomepageResponse>(new GetHomepageRequest()).ConfigureAwait(false);
                homepage = response?.Homepage;
            }

            return new GetHomepageQueryResult
            {
                Homepage = homepage
            };
        }
    }
}
