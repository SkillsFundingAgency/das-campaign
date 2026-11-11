using MediatR;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetHomepageQuery : IRequest<GetHomepageQueryResult>
    {
        public bool Preview { get; set; }
    }
}
