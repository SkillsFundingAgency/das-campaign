using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetPanelQuery : IRequest<GetPanelQueryResult>
    {
        public string Slug { get; set; }
        public bool Preview { get; set; }
    }
}
