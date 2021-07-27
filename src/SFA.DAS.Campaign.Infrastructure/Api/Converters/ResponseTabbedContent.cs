using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseTabbedContent
    {
        [JsonProperty("content")]
        public MainContent Content { get; set; }
        [JsonProperty("tabName")]
        public string TabName { get; set; }
        [JsonProperty("tabTitle")]
        public string TabTitle { get; set; }
    }
}
