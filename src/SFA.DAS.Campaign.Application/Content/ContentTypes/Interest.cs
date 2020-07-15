using System.Collections.Generic;
using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class Interest : ContentBase
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Asset Thumbnail { get; set; }
        public Document Body { get; set; }
        public List<string> Includes { get; set; }
        public Asset Image { get; set; }
        public RealStory RealStory { get; set; }
    }
}