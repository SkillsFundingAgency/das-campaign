﻿using System.Collections.Generic;
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
        [Range((int)RouteType.Apprentice, (int)RouteType.Employer)]
        [DisplayName("Registration type")]
        public RouteType Route { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You need to accept terms and conditions")]
        public bool AcceptTandCs { get; set; }

        public string ReturnUrl { get; set; }

        public int Version { get; set; }

        private Dictionary<string, string> GetValidationMessages()
        {
            return new Dictionary<string, string>
            {
                {"FirstName", "Enter your first name"},
                {"LastName", "Enter your last name"},
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
