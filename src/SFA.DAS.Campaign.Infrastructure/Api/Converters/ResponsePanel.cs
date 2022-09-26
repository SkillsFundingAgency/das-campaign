using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponsePanel
    {
        [JsonProperty("pageAttributes")]
        public PageAttributes PageAttributes { get; set; }

        [JsonProperty("mainContent")]
        public PanelContent MainContent { get; set; }
    }
}