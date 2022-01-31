
namespace SFA.DAS.Campaign.Domain.Interfaces
{
    public interface IMappingService
    {
        string GetStaticMapsUrl(double latitude, double longitude);
    }
}