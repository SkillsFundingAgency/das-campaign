using MediatR;
using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetSiteMapQuery : IRequest<GetSiteMapQueryResult<SiteMap>>
    {
        
    }
}
