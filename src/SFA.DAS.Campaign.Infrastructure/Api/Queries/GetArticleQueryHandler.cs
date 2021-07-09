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
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, GetArticleQueryResult<Article>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetArticleQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetArticleQueryResult<Article>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var canPreview = _config.Value.AllowPreview;
            Page<Article> article = null;

            if (canPreview && request.Preview)
            {
                article = await _apiClient.Get<Page<Article>>(new GetArticlesPreviewRequest(request.Hub, request.Slug)).ConfigureAwait(false);
            }
            
            article ??= await _apiClient.Get<Page<Article>>(new GetArticlesRequest(request.Hub, request.Slug))
                .ConfigureAwait(false);

            await article.FetchMenu(_apiClient);

            return new GetArticleQueryResult<Article>
            {
                Page = article,
            };
        }
    }
}
