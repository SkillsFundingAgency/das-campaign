using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
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
            var canPreview = _config.Value.AllowPreview;
            Page<LandingPage> landingPage = null;

            if (canPreview && request.Preview)
            {
                landingPage = await _apiClient.Get<Page<LandingPage>>(new GetLandingPagePreviewRequest(request.Hub, request.Slug)).ConfigureAwait(false);
            }

            if (landingPage == null)
            {
                landingPage = await _apiClient.Get<Page<LandingPage>>(new GetLandingPageRequest(request.Hub, request.Slug))
                    .ConfigureAwait(false);
            }

            await landingPage.FetchMenu(_apiClient);

            return new GetLandingPageQueryResult<LandingPage>
            {
                Page = landingPage
            };
        }
    }
}
