using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices.Interfaces
{
    public interface IProviderNameSearchService
    {
        Task<ProviderNameSearchResultsAndPagination> SearchProviderNameAndAliases(string searchTerm, int page, int? pageSize);
    }
}