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
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("image")]
        public ImageContent Image { get; set; }

        [JsonProperty("button")]
        public ButtonContent Button { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}