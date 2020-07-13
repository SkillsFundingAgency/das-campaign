using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class InfoPageSection : ContentBase
    {
        public string Title { get; set; }
        public Document Body { get; set; }
        public bool ShowHeaderLink { get; set; }
    }
}