using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel
    {
        public RegisterInterestModel()
        {
            ValidationMessages = GetValidationMessages();
        }

        public RegisterInterestModel(string returnUrl, int version, string controllerName, string actionName)
        {
            ReturnUrl = returnUrl;
            Version = version;
            Route = GetRouteFromControllerAndActionName(controllerName, actionName);
            ValidationMessages = GetValidationMessages();
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

        public int Version { get; set; }

        private string GetRouteFromControllerAndActionName(string controllerName, string actionName)
        {
            string route = "0";
           
            if (controllerName == "apprentice" || actionName == "apprentice" || controllerName == "parents")
            {
                route = "1";
            }
            else if (controllerName == "employer" || actionName == "employer")
            {
                route = "2";
            }
      
            return route;
        }

        private Dictionary<string, string> GetValidationMessages()
        {
            return new Dictionary<string, string>
            {
                {"FirstName", "Enter your first name"},
                {"LastName", "Enter your last name"},
                {"Email", "Enter your email address"},
                {"Route", "Select if you want to become an apprentice or employ an apprentice"},
                {"AcceptTandCs", "Confirm you would like to receive more information and are over 13 years old" }
            };
        }
    }
}
