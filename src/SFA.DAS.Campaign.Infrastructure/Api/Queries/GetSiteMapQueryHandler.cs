﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using SFA.DAS.Campaign.Infrastructure.Configuration;

namespace SFA.DAS.Campaign.Infrastructure.Api.Queries
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
