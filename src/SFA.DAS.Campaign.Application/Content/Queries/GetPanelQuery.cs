using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetPanelQuery : IRequest<GetPanelQueryResult<Panel>>
    {
        public string Slug { get; set; }
    }
}
