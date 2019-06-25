using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel
    {
        public RegisterInterestModel()
        {
            ValidationMessages = new Dictionary<string, string>
            {
                {"FirstName", "First Name"},
                {"LastName", "Last Name"},
                {"Email", "Email"},
                {"Route", "Type of user"},
                {"AcceptTandCs", "Terms and conditions"}
            };
        }

        public Dictionary<string, string> ValidationMessages { get; internal set; }

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
