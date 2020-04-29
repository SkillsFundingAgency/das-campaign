namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class LevyOptionViewModel
    {
        public GreaterThanThreeMillion GreaterThanThreeMillion { get; set; }
    }
    
    public enum GreaterThanThreeMillion
    {
        Yes,
        No
    }
}