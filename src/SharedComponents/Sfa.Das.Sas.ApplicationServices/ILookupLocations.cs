using System.Threading.Tasks;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices
{
    public interface ILookupLocations
    {
        Task<CoordinateResponse> GetLatLongFromPostCode(string postcode);
    }
}