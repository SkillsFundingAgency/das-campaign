using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Application.Content.ContentTypes
{
    public class RealStory : ContentBase
    {
        public string Title { get; set; }
        public Document Story { get; set; }
        public string Quote { get; set; }
        
        public string Author { get; set; }
        public string AuthorAge { get; set; }
        public string AuthorRole { get; set; }
        
        public string Location { get; set; }
    }
}