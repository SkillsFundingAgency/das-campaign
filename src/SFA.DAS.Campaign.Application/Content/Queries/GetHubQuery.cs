using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetHubQuery : IRequest<GetHubQueryResult<Hub>>
    {
        public string Hub { get; set; }
        public bool Preview { get ; set ; }
    }
}
