using System.Collections.Generic;
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
    public class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, GetAnnouncementsQueryResult>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetAnnouncementsQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetAnnouncementsQueryResult> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var allowPreview = _config.Value.AllowPreview;
            var forcePreview = _config.Value.ForcePreview;
            List<AnnouncementSummary> announcements = null;

            if ((allowPreview && request.Preview) || forcePreview)
            {
                var preview = await _apiClient.Get<GetAnnouncementsResponse>(new GetAnnouncementsPreviewRequest()).ConfigureAwait(false);
                announcements = preview?.Announcements?.Announcements;
            }

            if (announcements == null)
            {
                var response = await _apiClient.Get<GetAnnouncementsResponse>(new GetAnnouncementsRequest()).ConfigureAwait(false);
                announcements = response?.Announcements?.Announcements;
            }

            return new GetAnnouncementsQueryResult
            {
                Announcements = announcements ?? new List<AnnouncementSummary>()
            };
        }
    }
}
