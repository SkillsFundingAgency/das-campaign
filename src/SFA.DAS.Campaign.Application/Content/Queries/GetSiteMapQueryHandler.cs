using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetSiteMapQueryHandler : IRequestHandler<GetSiteMapQuery, GetSiteMapQueryResult<SiteMap>>
    {
        private readonly IApiClient _apiClient;
        
        public GetSiteMapQueryHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetSiteMapQueryResult<SiteMap>> Handle(GetSiteMapQuery request, CancellationToken cancellationToken)
        {
            var sitemap = await _apiClient.Get<Page<SiteMap>>(new GetSiteMapRequest()).ConfigureAwait(false);
            
            return new GetSiteMapQueryResult<SiteMap>
            {
                Page = sitemap
            };
        }
    }
}
