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

        public GetArticleQueryHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetArticleQueryResult<Article>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _apiClient.Get<Page<Article>>(new GetArticlesRequest(request.Hub, request.Slug))
                .ConfigureAwait(false);

            return new GetArticleQueryResult<Article>
            {
                Page = article
            };
        }
    }
}
