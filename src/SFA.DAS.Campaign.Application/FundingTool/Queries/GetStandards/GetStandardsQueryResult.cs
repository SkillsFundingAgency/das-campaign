using MediatR;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandards
{
    public class GetStandardsQueryResult : IRequest<GetStandardsQuery>
    {
        public List<Standard> Standards { get; set; }
    }
}
