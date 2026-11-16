using MediatR;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetAnnouncementQuery : IRequest<GetAnnouncementQueryResult>
    {
        public string Slug { get; set; }
        public bool Preview { get; set; }
    }
}
