using MediatR;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId
{
    public class GetStandardQueryResult : IRequest<GetStandardQuery>
    {
        public string Title { get; set; }
        public string StandardUId { get; set; }
        public int LarsCode { get; set; }
        public int Level { get; set; }
        public int TimeToComplete { get; set; }
        public int MaxFundingAvailable { get; set; }
    }
}
