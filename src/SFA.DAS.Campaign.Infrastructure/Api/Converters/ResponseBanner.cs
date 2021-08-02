using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Infrastructure.Api.Converters
{
    public class ResponseBanner
    {
        public List<Item> Items { get; set; }
        public string BackgroundColour { get; set; }
        public bool AllowUserToHideTheBanner { get; set; }
        public bool ShowOnTheHomepageOnly { get; set; }
        public string Title { get; set; }
    }
}
