using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel
    {
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [DisplayName("Email address")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Registration type")]
        public string Route { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You need to accept terms and conditions")]
        public bool AcceptTandCs { get; set; }

        public string ReturnUrl { get; set; }
    }
}
