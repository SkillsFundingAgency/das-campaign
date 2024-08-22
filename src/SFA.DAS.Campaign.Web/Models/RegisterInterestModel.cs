using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class RegisterInterestModel
    {
        public RegisterInterestModel()
        {
            ValidationMessages = GetValidationMessages();
        }

        public RegisterInterestModel(string returnUrl, int version, RouteType route, Menu menu, IEnumerable<Banner> bannerModels)
        {
            ReturnUrl = returnUrl;
            Version = version;
            Route = route;
            ValidationMessages = GetValidationMessages();
            Menu = menu;
            BannerModels = bannerModels;
        }

        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Dictionary<string, string> ValidationMessages { get; internal set; }

        [Required(ErrorMessage = "Enter your first name")]
        [DisplayName("Given name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name")]
        [DisplayName("Family name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Select the size of your company")]
        [DisplayName("Size Of Your Company")]
        public string SizeOfYourCompany { get; set; }
        [Required(ErrorMessage = "Select your industry")]
        [DisplayName("Industry")]
        public string Industry { get; set; }
        [Required(ErrorMessage = "Select your region")]
        [DisplayName("Location")]
        public string Location { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Enter your email address")]
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
                {"FirstName", "Enter your first name"},
                {"LastName", "Enter your last name"},
                {"Email", "Enter your email address"},
                {"SizeOfYourCompany", "Select the size of your company"},
                {"Location", "Select your region"},
                {"Industry", "Select your industry"},
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
