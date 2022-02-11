using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, GetMenuQueryResult<Menu>>
    {
        private readonly IApiClient _apiClient;

        public GetMenuQueryHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
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
