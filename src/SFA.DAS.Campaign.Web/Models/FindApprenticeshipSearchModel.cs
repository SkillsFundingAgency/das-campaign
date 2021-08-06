using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class FindApprenticeshipSearchModel
    {
        [Required(ErrorMessage = "Enter a postcode")]
        [DisplayName("Postcode")]
        [RegularExpression("\\b((?:(?:girGIR)|(?:[a-pr-uwyzA-PR-UWYZ])(?:(?:[0-9](?:[a-hjkpstuwA-HJKPSTUW]|[0-9])?)|(?:[a-hk-yA-HK-Y][0-9](?:[0-9]|[abehmnprv-yABEHMNPRV-Y])?)))) ?([0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2})\\b", ErrorMessage = "Enter a postcode, like AA1 1AA")]
        public string Postcode { get; set; }
        [Required]
        [DisplayName("Distance")]
        public int Distance { get; set; }
        [Required(ErrorMessage = "Select an interest")]
        [DisplayName("Interest")]
        public string Route { get; set; }

        public List<string> Routes { get ; set ; }

        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
    }
}
