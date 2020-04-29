namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class LevyOptionViewModel
    {
        public bool PreviouslySet { get; set; }
        public GreaterThanThreeMillion GreaterThanThreeMillion { get; set; }
    }
    
    public enum GreaterThanThreeMillion
    {
        Yes,
        No
    }
}