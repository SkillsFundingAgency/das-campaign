using System.Collections.Generic;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
   public class VacancySearchResult
    {
        public Location searchLocation { get; set; }
        public IList<VacancySearchResultItem> Results { get; set; }
    }
}
