using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseHub
    {
        [JsonProperty("pageAttributes")]
        public PageAttributes PageAttributes { get; set; }

        [JsonProperty("mainContent")]
        public HubContent MainContent { get; set; }
    }
    

    public class HubContent
    {
        [JsonProperty("headerImage")]
        public EmbeddedResource HeaderImage { get; set; }

        [JsonProperty("cards")]
        public List<RelatedArticle> Cards { get; set; }
    }
}
