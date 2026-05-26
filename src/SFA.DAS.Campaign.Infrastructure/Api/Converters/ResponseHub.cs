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

        public ResponseBanner BannerModels { get; set; }
    }
    

    public class HubContent
    {
        [JsonProperty("headerImage")]
        public Item HeaderImage { get; set; }

        [JsonProperty("cardsTitle")]
        public string CardsTitle { get; set; }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }

        [JsonProperty("cardsTitle2")]
        public string CardsTitle2 { get; set; }

        [JsonProperty("cards2")]
        public List<Card> Cards2 { get; set; }

        [JsonProperty("cardsTitle3")]
        public string CardsTitle3 { get; set; }

        [JsonProperty("cards3")]
        public List<Card> Cards3 { get; set; }
    }
}
