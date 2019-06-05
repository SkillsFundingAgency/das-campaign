using System.Collections.Generic;
using SFA.DAS.Campaign.Models.Vacancy;
using SFA.DAS.Campaign.Web.Models.Vacancy;

namespace SFA.DAS.Campaign.Web.Models
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            Location = new Location();
        }
        public int TotalResults { get; set; }
        public IList<VacancySearchResultItem> Results { get; set; }
        public string JsonResults { get; set; }
        public Location Location { get; set; }
        public string Route { get; set; }
        public string Postcode { get; set; }
        public int Distance { get; set; }
        public string StaticMapUrl { get; set; }
        public VacancySearchViewModel filterModel => new VacancySearchViewModel();
    }
}
