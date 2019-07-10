using Microsoft.Rest;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Infrastructure.Mappers;
using SFA.DAS.Vacancies.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacanciesApi;
using Location = SFA.DAS.Campaign.Domain.Vacancies.Location;

namespace SFA.DAS.Campaign.Infrastructure.Repositories
{
    public class VacanciesRepository : IVacanciesRepository
    {
        private const int _apiMaxPageSize = 250;
        private readonly ILivevacanciesAPI _vacanciesApi;
        private readonly IVacanciesMapper _vacanciesMapper;
        private readonly IGeocodeService _geocodeService;
        private readonly IMappingService _mappingService;
        private readonly IStandardsRepository _standardsService;

        public VacanciesRepository(ILivevacanciesAPI vacanciesApi, IVacanciesMapper vacanciesMapper,
            IGeocodeService geocodeService, IMappingService mappingService, IStandardsRepository standardsService)
        {
            _vacanciesApi = vacanciesApi;
            _vacanciesMapper = vacanciesMapper;
            _geocodeService = geocodeService;
            _mappingService = mappingService;
            _standardsService = standardsService;
        }

        public async Task<VacancySearchResult> GetByPostcode(string postcode, int distance)
        {

            var coordinates = await _geocodeService.GetFromPostCode(postcode);



            if (coordinates.ResponseCode == "OK")
            {
                var searchResults = new VacancySearchResult();
                searchResults.searchLocation = new Location()
                {
                    Latitude = coordinates.Coordinates.Lat,
                    Longitude = coordinates.Coordinates.Lon
                };

                int pageNumber = 1;
                var vacancyApiList = GetVacancyList(distance, coordinates, pageNumber);


                while (vacancyApiList.Count == _apiMaxPageSize && vacancyApiList.Max(s => s.DistanceInMiles < distance))
                {
                    pageNumber++;

                    var list = GetVacancyList(distance, coordinates, pageNumber);
                    vacancyApiList.AddRange(list);

                    if (list.Count < _apiMaxPageSize)
                    {
                        break;
                    }

                }

                searchResults.Results = vacancyApiList.Where(w => w.TrainingType == TrainingType.Standard)
                    .Select(_vacanciesMapper.Map)
                    .ToList();

                Parallel.ForEach(searchResults.Results,
                    vacancy => { vacancy.StaticMapUrl = _mappingService.GetStaticMapsUrl(vacancy.Location); });

                return searchResults;
            }

            return null;
        }

        public async Task<VacancySearchResult> GetByRoute(string routeId, string postcode, int distance)
        {
            var coordinates = await _geocodeService.GetFromPostCode(postcode);

            if (coordinates.ResponseCode == "OK")
            {
                var searchResults = new VacancySearchResult();
                searchResults.searchLocation = new Location()
                {
                    Latitude = coordinates.Coordinates.Lat,
                    Longitude = coordinates.Coordinates.Lon
                };

                int pageNumber = 1;
                var vacancyApiList = await GetVacancyListByRoute(routeId, distance, coordinates, pageNumber);

                while (vacancyApiList.Count == _apiMaxPageSize && vacancyApiList.Max(s => s.DistanceInMiles < distance))
                {
                    pageNumber++;

                    var list = await GetVacancyListByRoute(routeId, distance, coordinates, pageNumber);
                    vacancyApiList.AddRange(list);

                    if (list.Count < _apiMaxPageSize)
                    {
                        break;
                    }
                }

                searchResults.Results = vacancyApiList.Where(w => w.TrainingType == TrainingType.Standard)
                    .Select(_vacanciesMapper.Map)
                    .ToList();

                Parallel.ForEach(searchResults.Results,
                    vacancy => { vacancy.StaticMapUrl = _mappingService.GetStaticMapsUrl(vacancy.Location); });

                return searchResults;
            }
            return null;
        }


        private List<Result> GetVacancyList(int distance, CoordinatesResponse coordinates, int pageNumber = 1)
        {
            var result = (HttpOperationResponse<object>)_vacanciesApi.SearchApprenticeshipVacancies(
                coordinates.Coordinates.Lat, coordinates.Coordinates.Lon, pageNumber, 250, distance);


            var vacancyList = ((VacancySearchResults)(result).Body).Results.ToList();
            return vacancyList;
        }

        private async Task<List<Result>> GetVacancyListByRoute(string routeId, int distance,
            CoordinatesResponse coordinates, int pageNumber = 1)
        {
            var standards = await _standardsService.GetByRoute(routeId);

            var standardIds = string.Join(',', standards.Select(s => s.Id.ToString()));

            var result = (HttpOperationResponse<object>)_vacanciesApi.SearchApprenticeshipVacancies(
                coordinates.Coordinates.Lat, coordinates.Coordinates.Lon, pageNumber, 250, distance, standardIds);

            var vacancyList = ((VacancySearchResults)(result).Body).Results.ToList();
            return vacancyList;
        }
    }
}