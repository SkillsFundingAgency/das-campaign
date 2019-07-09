namespace SFA.DAS.Campaign.Infrastructure.Geocode.Configuration
{
    public interface IMappingConfiguration
    {
        string PrivateKey { get; }
        string ClientId { get; }
        string StaticHeight { get; }
        string StaticWidth { get; }
       string ApiKey { get; }
    }
}
