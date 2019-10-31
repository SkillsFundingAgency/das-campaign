using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Enums;

namespace SFA.DAS.Campaign.Domain.Vacancies
{
   public class VacancySearchResult
    {
        public Location searchLocation { get; set; }
        public Country Country { get; set; }
        public IList<VacancySearchResultItem> Results { get; set; }
    }
}
