using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Vacancies;

namespace SFA.DAS.Campaign.Domain.Interfaces
{
    public interface IMappingService
    {
        string GetStaticMapsUrl(Location coordinates);
        string GetStaticMapsUrl(IEnumerable<Location> locations);
        string GetStaticMapsUrl(IEnumerable<Location> locations, string height, string width);
    }
}