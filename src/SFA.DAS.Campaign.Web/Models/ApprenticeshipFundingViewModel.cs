using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ApprenticeshipFundingViewModel
    {
        //public List<string> Routes { get ; set ; } - get courses here

        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Page<Panel> Panel1 { get; set; }
        public Page<Panel> Panel2 { get; set; }
    }
}
