using System.Threading.Tasks;

namespace Sfa.Das.Sas.ApplicationServices.Services
{
    public interface IPostcodeService
    {
        Task<string> GetPostcodeStatus(string postcode);
    }
}