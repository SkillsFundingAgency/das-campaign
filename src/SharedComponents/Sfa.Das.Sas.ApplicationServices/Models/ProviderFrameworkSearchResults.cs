namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public class ProviderFrameworkSearchResults : BaseProviderSearchResults
    {
        public string Title { get; set; }

        public string FrameworkId { get; set; }

        public int FrameworkCode { get; set; }

        public string FrameworkName { get; set; }

        public string PathwayName { get; set; }

        public int FrameworkLevel { get; set; }

        public string FrameworkResponseCode { get; set; }
    }
}