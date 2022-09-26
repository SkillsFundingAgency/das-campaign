﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class PanelContent
    {
        public PanelContent()
        {
            Items = new List<Item>();
        }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("image")]
        public ImageContent Image { get; set; }

        [JsonProperty("button")]
        public ButtonContent Button { get; set; }
    }
}