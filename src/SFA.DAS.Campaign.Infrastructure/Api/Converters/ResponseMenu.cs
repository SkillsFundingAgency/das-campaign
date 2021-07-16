using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseMenu
    {
        [JsonProperty("mainContent")]
        public MenuContent MainContent { get; set; }
    }

    public class MenuContent
    {
        [JsonProperty("topLevel")]
        public List<SiteMapPage> TopLevel { get; set; }
        [JsonProperty("influencers")]
        public List<SiteMapPage> Influencers { get; set; }
        [JsonProperty("apprentices")]
        public List<SiteMapPage> Apprentices { get; set; }
        [JsonProperty("employers")]
        public List<SiteMapPage> Employers { get; set; }
    }
}
