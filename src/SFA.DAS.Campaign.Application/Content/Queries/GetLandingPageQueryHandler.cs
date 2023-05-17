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
    public class GetLandingPageQueryHandler : IRequestHandler<GetLandingPageQuery, GetLandingPageQueryResult<LandingPage>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetLandingPageQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetLandingPageQueryResult<LandingPage>> Handle(GetLandingPageQuery request, CancellationToken cancellationToken)
        {
            var allowPreview = _config.Value.AllowPreview;
            var forcePreview = _config.Value.ForcePreview;
            Page<LandingPage> landingPage = null;

            if (allowPreview && request.Preview || forcePreview)
            {
                landingPage = await _apiClient.Get<Page<LandingPage>>(new GetLandingPagePreviewRequest(request.Hub, request.Slug)).ConfigureAwait(false);
            }

            if (landingPage == null)
            {
                landingPage = await _apiClient.Get<Page<LandingPage>>(new GetLandingPageRequest(request.Hub, request.Slug))
                    .ConfigureAwait(false);
            }

            return new GetLandingPageQueryResult<LandingPage>
            {
                Page = landingPage
            };
        }
    }
}
