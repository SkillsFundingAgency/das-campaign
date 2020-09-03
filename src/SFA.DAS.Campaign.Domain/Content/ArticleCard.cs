namespace SFA.DAS.Campaign.Domain.Content
{
    public class ArticleCard
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string LandingPageTitle { get; set; }
        public string LandingPageSlug { get; set; }
        public HubType HubType { get; set; }
    }
}