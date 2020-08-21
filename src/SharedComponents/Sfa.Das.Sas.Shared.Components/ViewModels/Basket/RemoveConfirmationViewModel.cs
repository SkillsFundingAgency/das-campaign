using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Basket
{
    public class RemoveConfirmationViewModel 
    {
        public ApprenticeshipFavouriteRead ApprenticeshipItem { get; }
        public string ReturnPath { get; set; }
        public string ApprenticeshipId { get; set; }

        public RemoveConfirmationViewModel()
        {
            
        }
        
        public RemoveConfirmationViewModel(ApprenticeshipFavouriteRead apprenticeshipItem, string returnPath)
        {
            ApprenticeshipItem = apprenticeshipItem;
            ReturnPath = returnPath;
        }
    }
}