using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponsePanel
    {
        [JsonProperty("mainContent")]
        public PanelContent MainContent { get; set; }
    }
}