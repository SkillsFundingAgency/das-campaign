namespace SFA.DAS.Campaign.Domain.Configuration.Models
{
    public class MappingConfiguration : IMappingConfiguration
    {
        public string PrivateKey { get; set; }
        public string ClientId { get; set; }
        public string StaticHeight { get; set; }
        public string StaticWidth { get; set; }
        public string ApiKey { get; set; }
    }
}
