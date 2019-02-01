using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Campaign.Models.Vacancy
{
    public class VacancySearchResultItem
    {
        public long VacancyReference { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string TrainingTitle { get; set; }
        public Uri VacancyUrl { get; set; }
        public Location Location { get; set; }
        public double DistanceInMiles { get; set; }

        public string EmployerName { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ApplicationClosingDate { get; set; }
        public string StaticMapUrl { get; set; }
    }

    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
