using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Content.HtmlControl
{
    public class Button : IHtmlControl
    {
        public Button()
        {
            Styles = new List<string>();
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public List<string> Styles { get; set; }
    }
}
