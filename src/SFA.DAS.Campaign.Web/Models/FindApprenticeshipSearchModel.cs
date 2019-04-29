using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Models
{
    public class FindApprenticeshipSearchModel
    {
        [Required]
        [DisplayName("Postcode")]
        [RegularExpression("\\b((?:(?:girGIR)|(?:[a-pr-uwyzA-PR-UWYZ])(?:(?:[0-9](?:[a-hjkpstuwA-HJKPSTUW]|[0-9])?)|(?:[a-hk-yA-HK-Y][0-9](?:[0-9]|[abehmnprv-yABEHMNPRV-Y])?)))) ?([0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2})\\b", ErrorMessage = "You must enter a valid postcode")]
        public string Postcode { get; set; }
        [Required]
        [DisplayName("Distance")]
        public int Distance { get; set; }
        [Required]
        [DisplayName("Interest")]
        public string Route { get; set; }

    }
}
