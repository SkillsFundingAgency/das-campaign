using System.Threading.Tasks;
using Sfa.Das.Sas.ApplicationServices.Models;

namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Collections.Generic;

    public interface IApprenticeshipSearchProvider
    {
        Task<ApprenticeshipSearchResults> SearchByKeyword(string keywords, int page, int take, int order, List<int> selectedLevels);
    }
}