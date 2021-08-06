using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInform
{
    public class LevyOptionViewModel
    {
        public bool OptionChosenByUser { get; set; }
        [Required(ErrorMessage = "Please select an option before continuing")]
        public LevyStatus? LevyStatus { get; set; }

        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
    }
    
    public enum LevyStatus
    {
        Levy,
        NonLevy,
        DontKnow
    }
}