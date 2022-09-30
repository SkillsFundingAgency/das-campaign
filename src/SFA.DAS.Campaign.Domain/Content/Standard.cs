namespace SFA.DAS.Campaign.Domain.Content
{
    public class StandardResponse
    {
        public string Title { get; set; }
        public string StandardUId { get; set; }
        public int LarsCode { get; set; }
        public int Level { get; set; }
        public int TimeToComplete { get; set; }
        public int MaxFundingAvailable { get; set; }
    }
}
