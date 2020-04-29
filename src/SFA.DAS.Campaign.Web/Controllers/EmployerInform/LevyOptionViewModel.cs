namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class LevyOptionViewModel
    {
        public bool PreviouslySet { get; set; }
        public LevyStatus LevyStatus { get; set; }
    }
    
    public enum LevyStatus
    {
        Levy,
        NoneLevy
    }
}