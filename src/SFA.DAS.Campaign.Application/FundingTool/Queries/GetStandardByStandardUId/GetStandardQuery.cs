using MediatR;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId
{
    public class GetStandardQuery : IRequest<GetStandardQueryResult>
    {
        public string StandardUId { get; set; }
    }
}
