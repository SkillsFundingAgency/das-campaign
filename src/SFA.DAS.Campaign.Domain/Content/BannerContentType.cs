using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class BannerContentType : IContentType
    {
        public BannerContentType()
        {
            Content = new List<IHtmlControl>();
        }

        public List<IHtmlControl> Content { get; set; }
        public string BackgroundColour { get; set; }
        public bool AllowUserToHideTheBanner { get; set; }
        public bool ShowOnTheHomepageOnly { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
    }
}