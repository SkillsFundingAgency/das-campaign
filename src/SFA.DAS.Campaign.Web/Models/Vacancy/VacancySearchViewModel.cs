using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SFA.DAS.Campaign.Web.Models.Vacancy
{
    public class VacancySearchViewModel
    {
        public string Keywords { get; set; }
        public string Postcode { get; set; }
        public int Distance { get; set; }
        public string IndustrySectors { get; set; }
        public SelectList DistanceOptions => getDistanceSelectList(true);

        public SelectList IndustrySectorsOptions => getIndustrySectorsList();


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

            //if (includeEngland)
            //{
            //    distanceOptions.Add(new { WithinDistance = 0, Name = "England" });
            //}

            var distances = new SelectList(
                distanceOptions.ToArray(),
                "WithinDistance",
                "Name"
            );

            return distances;
        }

        private SelectList getIndustrySectorsList()
        {
            var distanceOptions = new List<object>
            {
                new {WithinDistance = 2, Name = "Construction"},
                new {WithinDistance = 5, Name = "Hospitality"},
                new {WithinDistance = 10, Name = "Public Sector"},
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
