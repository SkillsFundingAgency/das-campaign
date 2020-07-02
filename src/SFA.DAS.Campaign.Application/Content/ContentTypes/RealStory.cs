using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class RealStory : ContentBase
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Document Story { get; set; }
        public string AgeLocation { get; set; }
        public string YoutubeLink { get; set; }
        public string ThumbnailUrl { get; set; }
        public string EmbedUrl { get; set; }
    }
}