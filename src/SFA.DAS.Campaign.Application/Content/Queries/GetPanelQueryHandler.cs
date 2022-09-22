using MediatR;
using SFA.DAS.Campaign.Domain.Api.Interfaces;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Infrastructure.Api.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetPanelQueryHandler : IRequestHandler<GetPanelQuery, GetPanelQueryResult<Panel>>
    {
        private readonly IApiClient _apiClient;

        public GetPanelQueryHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<GetPanelQueryResult<Panel>> Handle(GetPanelQuery request, CancellationToken cancellationToken)
        {
            var result = await _apiClient.Get<Page<Panel>>(new GetPanelRequest(request.Slug)).ConfigureAwait(false);

            return new GetPanelQueryResult<Panel>
            {
                Page = result
            };
        }
    }
}
