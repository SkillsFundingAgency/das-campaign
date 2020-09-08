using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel
    {
        public RegisterInterestModel()
        {
            ValidationMessages = GetValidationMessages();
        }

        public RegisterInterestModel(string returnUrl, int version, RouteType route)
        {
            ReturnUrl = returnUrl;
            Version = version;
            Route = route;
            ValidationMessages = GetValidationMessages();
        }

        public Dictionary<string, string> ValidationMessages { get; internal set; }

        [Required]
        [DisplayName("Given name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Family name")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        [DisplayName("Email address")]
        public string Email { get; set; }
        [Required]
        [Range((int)RouteType.Apprentice, (int)RouteType.Employer)]
        [DisplayName("Registration type")]
        public RouteType Route { get; set; }

        public bool IncludeInUR { get; set; }
        
        public string ReturnUrl { get; set; }

        public int Version { get; set; }

        public bool ShowRouteQuestion { get; set; }

        private Dictionary<string, string> GetValidationMessages()
        {
            return new Dictionary<string, string>
            {
                {"FirstName", "Enter your given name"},
                {"LastName", "Enter your family name"},
                {"Email", "Enter your email address"},
                {"Route", "Select if you want to become an apprentice or employ an apprentice"},
                {"AcceptTandCs", "Confirm you would like to receive more information" }
            };
        }
    }

    public enum RouteType
    {
        None = 0,
        Apprentice = 1,
        Parent = 1,
        Employer = 2
    }

}
