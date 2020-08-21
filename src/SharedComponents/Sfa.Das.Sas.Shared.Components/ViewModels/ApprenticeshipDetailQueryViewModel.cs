using Sfa.Das.Sas.ApplicationServices.Commands;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.Shared.Components.ViewModels
{
    public class ApprenticeshipDetailQueryViewModel
    {
        public string Id { get; set; }
        public ApprenticeshipType Type { get; set; }
        public AddOrRemoveFavouriteInBasketResponse AddRemoveBasketResponse { get; set; }
    }
}