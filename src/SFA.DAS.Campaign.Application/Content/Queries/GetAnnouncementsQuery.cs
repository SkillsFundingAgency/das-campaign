using MediatR;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetAnnouncementsQuery : IRequest<GetAnnouncementsQueryResult>
    {
        public bool Preview { get; set; }
    }
}
