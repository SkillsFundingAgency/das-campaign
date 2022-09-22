using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ApprenticeshipFundingViewModel
    {
        public List<string> Routes { get ; set ; }

        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Panel Panel1 { get; set; }
        public Panel Panel2 { get; set; }

        //add panel1 and panel2 and a list of courses to populate the type ahead
    }
}
