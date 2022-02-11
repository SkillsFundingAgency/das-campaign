using SFA.DAS.Campaign.Domain.Content;

namespace SFA.DAS.Campaign.Application.Content.Queries
{
    public class GetSiteMapQueryResult<T> where T: IContentType
    {
        public Page<T> Page { get; set; }
    }
}
