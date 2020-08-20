using Contentful.Core.Models;

namespace SFA.DAS.Campaign.Domain.Content
{
    public class Page<T>
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public Asset HeroImage { get; set; }
        public Navigation Navigation { get; set; }
        public T Content { get; set; }
        public HubType Hub { get; set; }
        
    }

    public enum HubType
    {
        Home,
        Apprentice,
        Employer,
        Parents
    }
}
