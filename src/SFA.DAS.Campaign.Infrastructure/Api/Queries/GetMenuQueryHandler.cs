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
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, GetMenuQueryResult<Menu>>
    {
        private readonly IApiClient _apiClient;
        private readonly IOptions<CampaignConfiguration> _config;

        public GetMenuQueryHandler(IApiClient apiClient, IOptions<CampaignConfiguration> config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<GetMenuQueryResult<Menu>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var menu = new Page<Menu>();

            await menu.FetchMenu(_apiClient);

            return new GetMenuQueryResult<Menu>
            {
                Page = menu,
            };
        }
    }
}
