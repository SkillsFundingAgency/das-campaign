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

        public string Postcode { get; set; }
        [Required]
        [DisplayName("Distance")]
        public int Distance { get; set; }
        [Required]
        [DisplayName("Interest")]
        public string Route { get; set; }

    }
}
