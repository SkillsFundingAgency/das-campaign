using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SFA.DAS.Campaign.Web.Models.Vacancy
{
    public class VacancySearchViewModel
    {
        public string Postcode { get; set; }
        public int Distance { get; set; }
        public SelectList DistanceOptions => getDistanceSelectList(true);

        
        private SelectList getDistanceSelectList(bool includeEngland)
        {
            var distanceOptions = new List<object>
            {
                new {WithinDistance = 2, Name = "2 miles"},
                new {WithinDistance = 5, Name = "5 miles"},
                new {WithinDistance = 10, Name = "10 miles"},
                new {WithinDistance = 15, Name = "15 miles"},
                new {WithinDistance = 20, Name = "20 miles"},
                new {WithinDistance = 30, Name = "30 miles"},
                new {WithinDistance = 40, Name = "40 miles"}
            };

            var distances = new SelectList(
                distanceOptions.ToArray(),
                "WithinDistance",
                "Name"
            );

            return distances;
        }
    }
}
