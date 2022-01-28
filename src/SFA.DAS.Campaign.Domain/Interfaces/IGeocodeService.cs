using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Models.Geocode;

namespace SFA.DAS.Campaign.Domain.Interfaces
{
    public interface IGeocodeService
    {
        Task<CoordinatesResponse> GetFromPostCode(string postcode);
    }
}