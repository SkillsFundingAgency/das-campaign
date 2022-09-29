using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ApprenticeshipFundingViewModel
    {
        public List<Standard> Standards { get; set; }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public Panel Panel1 { get; set; }
        public Panel Panel2 { get; set; }
        public Panel Panel3 { get; set; }

    }
}
