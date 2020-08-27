namespace SFA.DAS.Campaign.Domain.Content
{
    public class Page<T>
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public HubType HubType { get; set; }
        public T Content { get; set; } 
    }


    public enum HubType
    {
        Home,
        Employers,
        Apprentices
    }
}