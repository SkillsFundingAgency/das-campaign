using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
{
    public class GetArticlePreviewQueryHandler : IRequestHandler<GetArticlePreviewQuery, GetArticlePreviewQueryResult<Article>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetArticlePreviewQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetArticlePreviewQueryResult<Article>> Handle(GetArticlePreviewQuery request, CancellationToken cancellationToken)
        {
            var canPreview = _config.Value.OuterApi.AllowPreview;
            Page<Article> article = null;

            if (canPreview)
            {
                article = await _apiClient.Get<Page<Article>>(new GetArticlesPreviewRequest(request.Hub, request.Slug)).ConfigureAwait(false);
            }
            
            if (article == null && canPreview)
            {
                article = await _apiClient.Get<Page<Article>>(new GetArticlesRequest(request.Hub, request.Slug))
                    .ConfigureAwait(false);
            }
            
            return new GetArticlePreviewQueryResult<Article>
            {
                Page = article
            };
        }
    }
}
