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
    public class GetAnnouncementQueryHandler : IRequestHandler<GetAnnouncementQuery, GetAnnouncementQueryResult>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetAnnouncementQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetAnnouncementQueryResult> Handle(GetAnnouncementQuery request, CancellationToken cancellationToken)
        {
            var allowPreview = _config.Value.AllowPreview;
            var forcePreview = _config.Value.ForcePreview;
            Announcement announcement = null;

            if ((allowPreview && request.Preview) || forcePreview)
            {
                announcement = await _apiClient.Get<Announcement>(new GetAnnouncementPreviewRequest(request.Slug)).ConfigureAwait(false);
            }

            announcement ??= await _apiClient.Get<Announcement>(new GetAnnouncementRequest(request.Slug)).ConfigureAwait(false);

            return new GetAnnouncementQueryResult
            {
                Announcement = announcement
            };
        }
    }
}
