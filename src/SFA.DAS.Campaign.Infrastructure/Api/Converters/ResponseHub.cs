using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseHub
    {
        [JsonProperty("pageAttributes")]
        public PageAttributes PageAttributes { get; set; }

        [JsonProperty("mainContent")]
        public HubContent MainContent { get; set; }

        [JsonProperty("menuContent")]
        public MenuContent MenuContent { get; set; }
    }
    

    public class HubContent
    {
        [JsonProperty("headerImage")]
        public Item HeaderImage { get; set; }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }
    }
}
