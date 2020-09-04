namespace SFA.DAS.Campaign.Domain.Content
{
    public class ArticleCard
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string LandingPageTitle { get; set; }
        public string LandingPageSlug { get; set; }
        public HubType HubType { get; set; }
        public string Slug { get; set; }

        public string LandingPageUrl => "/" + HubType.ToString().ToLower() + "/" + LandingPageSlug;
        public string ArticleUrl => "/" + HubType.ToString().ToLower() + "/" + Slug;
    }
}