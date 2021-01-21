using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices
{
    public interface IApprenticeshipSearchService
    {
        Task<ApprenticeshipSearchResults> SearchByKeyword(string keywords, int page, int take, int order, List<int> selectedLevels);
    }
}
