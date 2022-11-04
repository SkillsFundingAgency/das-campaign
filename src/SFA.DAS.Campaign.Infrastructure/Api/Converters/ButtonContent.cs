using Newtonsoft.Json;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ButtonContent
    {
        public ButtonContent()
        {
            Styles = new List<string>();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("styles")]
        public List<string> Styles { get; set; }
    }
}
