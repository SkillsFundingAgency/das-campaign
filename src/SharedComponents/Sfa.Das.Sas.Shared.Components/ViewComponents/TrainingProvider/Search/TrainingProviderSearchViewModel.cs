using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sfa.Das.Sas.Shared.Components.ViewModels;

namespace Sfa.Das.Sas.Shared.Components.ViewComponents.TrainingProvider.Search
{
    public class TrainingProviderSearchViewModel : SearchQueryViewModel
    {
        public string SearchRoute { get; set; } = "/TrainingProvider/Search";

        [Required]
        [DisplayName("Postcode")]
        [RegularExpression("\\b((?:(?:girGIR)|(?:[a-pr-uwyzA-PR-UWYZ])(?:(?:[0-9](?:[a-hjkpstuwA-HJKPSTUW]|[0-9])?)|(?:[a-hk-yA-HK-Y][0-9](?:[0-9]|[abehmnprv-yABEHMNPRV-Y])?)))) ?([0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2})\\b", ErrorMessage = "You must enter a valid postcode")]
        [Remote(action: "ValidatePostcode",controller:"TrainingProvider",ErrorMessage = "You must enter a valid postcode")]
        public string Postcode { get; set; }

        [Required]
        public string ApprenticeshipId { get; set; }

        [Required]
        public bool IsLevyPayer { get; set; }

        public bool NationalProvidersOnly { get; set; }
    }
}