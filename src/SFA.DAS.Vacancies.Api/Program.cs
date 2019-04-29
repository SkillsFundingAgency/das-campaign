using System;
using System.Net.Http;
using SFA.DAS.Vacancies.Api.Models;
using VacanciesApi;

namespace SFA.DAS.Vacancies.Api
{
    class Program
    {
        static void Main(string[] args)
        {

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://www.test.com");
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "a38ac93176f04689a7d6cb3b53e60033");

            var client = new LivevacanciesAPI(httpClient, true);

            var result = (VacancySearchResults)client.SearchApprenticeshipVacanciesAsync(latitude: 52.3922196, longitude: -0.5352921, distanceInMiles: 10).Result.Body;


           Console.WriteLine($"Total Results: {result.TotalMatched},Total pages: {result.TotalPages},This page total: {result.TotalReturned}");
            foreach (var vacancySearchResult in result.Results)
            {
                Console.WriteLine($"Title: {vacancySearchResult.Title} - {vacancySearchResult.EmployerName} - {vacancySearchResult.TrainingTitle}");
            }
          
        }
    }
}
