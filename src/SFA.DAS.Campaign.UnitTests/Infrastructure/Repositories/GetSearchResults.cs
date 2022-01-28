using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Rest;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.Models.Geocode;
using SFA.DAS.Vacancies.Api.Models;

namespace SFA.DAS.Campaign.Infrastructure.UnitTests.Repositories
{
    public static class GetSearchResults
    {
        public static HttpOperationResponse<object> MockSearchResults(CoordinatesResponse coordinatesResponse, int searchResultsCount = 200)
        {
            var _result = new HttpOperationResponse<object>();

            _result.Request = new HttpRequestMessage();
            _result.Response = new HttpResponseMessage(HttpStatusCode.OK);

            var vacancyResults = new VacancySearchResults();

            var results = new List<Result>();


            while (results.Count != searchResultsCount)
            {
                var random = new Random();
                var id = results.Count + 1;
                results.Add(new Result()
                {
                    Location = new Vacancies.Api.Models.Location() { Latitude = coordinatesResponse.Coordinates.Lat, Longitude = coordinatesResponse.Coordinates.Lon },
                    Title = $"Vacancy {id}",
                    ShortDescription = "short description of vacancy",
                    DistanceInMiles = random.NextDouble() * 100,
                    VacancyReference = id,
                    TrainingType = (id % 2 == 0) ? TrainingType.Framework : TrainingType.Standard
                });
            }

            vacancyResults.Results = results.ToArray();
            _result.Body = vacancyResults;

            return _result;
        }
    }
}