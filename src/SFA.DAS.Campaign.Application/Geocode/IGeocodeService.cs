using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Application.Geocode
{
    public interface IGeocodeService
    {
        Task<CoordinatesResponse> GetFromPostCode(string postcode);
    }
}