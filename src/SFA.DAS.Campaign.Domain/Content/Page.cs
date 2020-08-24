namespace SFA.DAS.Campaign.Domain.Content
{
    public class Page<T>
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string HubType { get; set; }
        public string LandingPageSlug { get; set; }
        public string LandingPageTitle { get; set; }
        public T Content { get; set; } 
    }
}