using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Models.Vacancy
{
   public class VacancySearchResult
    {
        public Location searchLocation { get; set; }
        public IList<VacancySearchResultItem> Results { get; set; }
    }
}
