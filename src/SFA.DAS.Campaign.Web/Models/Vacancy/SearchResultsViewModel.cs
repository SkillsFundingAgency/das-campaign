using System.Collections.Generic;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Domain.Enums;
using SFA.DAS.Campaign.Domain.Vacancies;
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
        public Country Country { get; set; }
        public List<string> Routes { get ; set ; }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
    }
}
