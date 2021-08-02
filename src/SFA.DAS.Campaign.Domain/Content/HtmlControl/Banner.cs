using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Banner : IHtmlControl
    {
        public Banner()
        {
            Content = new List<IHtmlControl>();
        }

        public List<IHtmlControl> Content { get; set; }
        public string BackgroundColour { get; set; }
        public bool AllowUserToHideTheBanner { get; set; }
        public bool ShowOnTheHomepageOnly { get; set; }
        public string Title { get; set; }
    }
}
