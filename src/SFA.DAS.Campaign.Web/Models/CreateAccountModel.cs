using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Web.Models
{
    public class CreateAccountModel
    {
        public string BaseEmployerAccountUrl { get; set; }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
    }
}
