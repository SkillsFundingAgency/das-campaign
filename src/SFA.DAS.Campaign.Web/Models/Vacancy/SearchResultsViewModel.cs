using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Campaign.Application.Vacancies.Queries.GetVacancies;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Domain.Enums;

namespace SFA.DAS.Campaign.Web.Models
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            Location = new Location();
        }
        public int TotalResults { get; set; }
        public IList<SearchResultItem> Results { get; set; }
        public Location Location { get; set; }
        public string Route { get; set; }
        public string Postcode { get; set; }
        public int Distance { get; set; }
        public Country Country { get; set; }
        public List<string> Routes { get ; set ; }
        public Menu Menu { get; set; }
        public IEnumerable<Banner> BannerModels { get; set; }
        public static implicit operator SearchResultsViewModel(GetVacanciesQueryResult source)
        {
            return new SearchResultsViewModel
            {
                TotalResults = source.TotalFound,
                Location = source.Location != null ? new Location
                {
                    Latitude = source.Location.GeoPoint.FirstOrDefault(),
                    Longitude = source.Location.GeoPoint.LastOrDefault()
                } : null,
                CountryName = source.Location?.Country ?? "",
                Country = source.Location != null ? MapToCountry(source.Location.Country) : Country.England,
                Routes = source.Routes.Select(c=>c.Name).ToList(),
                Results = source.Vacancies.Select(c=>(SearchResultItem)c).ToList()
            };
        }

        public string CountryUrl => GetCountryUrl(Country);
        public string CountryName { get; set; }
        
        private string GetCountryUrl(Country country)
        {
            switch (country)
            {
                case Country.Wales:
                    return "https://careerswales.gov.wales/apprenticeship-search";
                case Country.Scotland:
                    return "https://www.apprenticeships.scot/find-a-vacancy/";
                case Country.NorthernIreland:
                    return "https://www.nidirect.gov.uk/services/search-apprenticeship-opportunities";
                default:
                    return "Other";
            }
        }
        
        private static Country MapToCountry(string country)
        {
            switch (country)
            {
                case "England":
                    return Country.England;
                case "Wales":
                    return Country.Wales;
                case "Scotland":
                    return Country.Scotland;
                case "Northern Ireland":
                    return Country.NorthernIreland;
                default:
                    return Country.Other;
            }
        }
    }
    
    public class SearchResultItem
    {
        public string VacancyReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubCategory { get; set; }
        public Uri VacancyUrl { get; set; }
        public Location Location { get; set; }
        public double Distance { get; set; }

        public string EmployerName { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string StaticMapUrl { get; set; }

        public static implicit operator SearchResultItem(Infrastructure.Api.Responses.Vacancy source)
        {
            return new SearchResultItem
            {
                VacancyReference = source.VacancyReference,
                Title = source.Title,
                EmployerName = source.EmployerName,
                PostedDate = source.PostedDate,
                ClosingDate = source.ClosingDate,
                StartDate = source.StartDate,
                Distance = source.Distance ?? 0,
                Description = source.Description,
                SubCategory = source.SubCategory,
                VacancyUrl = new Uri(source.VacancyUrl) ,
                Location = source.Location != null  ? new Location
                {
                    Latitude = source.Location.Lat,
                    Longitude = source.Location.Lon
                } : null
            };
        }
    }

    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
