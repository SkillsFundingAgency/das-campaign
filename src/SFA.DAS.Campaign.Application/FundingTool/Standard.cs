using SFA.DAS.Campaign.Application.FundingTool.Queries.GetStandardByStandardUId;
using SFA.DAS.Campaign.Domain.Content;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.FundingTool
{
    public class Standard
    {
        public string Title { get; set; }
        public int LarsCode { get; set; }
        public string StandardUId { get; set; }
        public int Level { get; set; }
        public int Duration { get; set; }
        public int MaxFunding { get; set; }

        public static implicit operator Standard(Task<StandardResponse> standard)
        {
            return new Standard
            {
                Title = standard.Result.Title,
                LarsCode = standard.Result.LarsCode,
                StandardUId = standard.Result.StandardUId,
                Level = standard.Result.Level,
                Duration = standard.Result.TimeToComplete,
                MaxFunding = standard.Result.MaxFundingAvailable
            };
        }

        public static implicit operator Standard(Task<GetStandardQueryResult> standard)
        {
            return new Standard
            {
                Title = standard.Result.Title,
                LarsCode = standard.Result.LarsCode,
                StandardUId = standard.Result.StandardUId,
                Level = standard.Result.Level,
                Duration = standard.Result.TimeToComplete,
                MaxFunding = standard.Result.MaxFundingAvailable
            };
        }
    }
}
