namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class LevyOption
    {
        public bool OptionChosenByUser { get; set; }
        public LevyStatus? LevyStatus { get; set; }
    }
    
    public enum LevyStatus
    {
        Levy,
        NonLevy,
        DontKnow
    }
}