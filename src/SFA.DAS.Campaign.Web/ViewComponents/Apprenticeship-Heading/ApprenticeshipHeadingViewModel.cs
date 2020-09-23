using Sfa.Das.Sas.ApplicationServices.Commands;

namespace SFA.DAS.Campaign.Web.ViewComponents
{
    public class ApprenticeshipHeadingViewModel
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int Level { get; set; }
        public AddOrRemoveFavouriteInBasketResponse AddRemoveBasketResponse { get; set; }
        public string BackUrl { get; set; }
    }
}   
