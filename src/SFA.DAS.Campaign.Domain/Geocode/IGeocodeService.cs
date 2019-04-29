using System.Threading.Tasks;
using SFA.DAS.Campaign.Models.Geocode;

namespace SFA.DAS.Campaign.Domain.Geocode
{
    public interface IGeocodeService
    {
        Task<CoordinatesResponse> GetFromPostCode(string postcode);
    }
}