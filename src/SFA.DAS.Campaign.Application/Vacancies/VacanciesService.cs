using Microsoft.Rest;
using SFA.DAS.Campaign.Application.Geocode;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Models.Geocode;
using SFA.DAS.Campaign.Models.Vacancy;
using SFA.DAS.Vacancies.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VacanciesApi;

namespace SFA.DAS.Campaign.Application.Vacancies
{
    public class VacanciesService : IVacanciesService
    {
        private readonly ILivevacanciesAPI _vacanciesApi;
        private readonly IVacanciesMapper _vacanciesMapper;
        private readonly IGeocodeService _geocodeService;
        private readonly IMappingService _mappingService;
        private readonly IStandardsService _standardsService;

        public VacanciesService(ILivevacanciesAPI vacanciesApi, IVacanciesMapper vacanciesMapper,
            IGeocodeService geocodeService, IMappingService mappingService,IStandardsService standardsService)
        {
            _vacanciesApi = vacanciesApi;
            _vacanciesMapper = vacanciesMapper;
            _geocodeService = geocodeService;
            _mappingService = mappingService;
           _standardsService = standardsService;
        }

        public async Task<IList<VacancySearchResultItem>> GetByPostcode(string postcode, int distance)
        {

            var coordinates = await _geocodeService.GetFromPostCode(postcode);

            int pageNumber = 1;
            var vacancyApiList = GetVacancyList(distance, coordinates, pageNumber);


            while (vacancyApiList.Count == 250 && vacancyApiList.Max(s => s.DistanceInMiles < distance))
            {
                pageNumber++;

                var list = GetVacancyList(distance, coordinates, pageNumber);
                vacancyApiList.AddRange(list);

                if (list.Count < 250)
                {
                    break;
                }

            }

            var vacancyList = vacancyApiList.Where(w => w.TrainingType == TrainingType.Standard)
                .Select(_vacanciesMapper.Map)
                .ToList();

            Parallel.ForEach(vacancyList,
                vacancy => { vacancy.StaticMapUrl = _mappingService.GetStaticMapsUrl(vacancy.Location); });

            return vacancyList;
        }

        public async Task<IList<VacancySearchResultItem>> GetByRoute(string routeId, string postcode, int distance)
        {
            var coordinates = await _geocodeService.GetFromPostCode(postcode);

            int pageNumber = 1;
            var vacancyApiList = await GetVacancyListByRoute(routeId,distance, coordinates, pageNumber);


            while (vacancyApiList.Count == 250 && vacancyApiList.Max(s => s.DistanceInMiles < distance))
            {
                pageNumber++;

                var list = await GetVacancyListByRoute(routeId,distance, coordinates, pageNumber);
                vacancyApiList.AddRange(list);

                if (list.Count < 250)
                {
                    break;
                }

            }

            var vacancyList = vacancyApiList.Where(w => w.TrainingType == TrainingType.Standard)
                .Select(_vacanciesMapper.Map)
                .ToList();

            Parallel.ForEach(vacancyList,
                vacancy => { vacancy.StaticMapUrl = _mappingService.GetStaticMapsUrl(vacancy.Location); });

            return vacancyList;
        }


        private List<Result> GetVacancyList(int distance, CoordinatesResponse coordinates, int pageNumber = 1)
        {
            var result = (HttpOperationResponse<object>) _vacanciesApi.SearchApprenticeshipVacancies(
                coordinates.Coordinates.Lat, coordinates.Coordinates.Lon, pageNumber, 250, distance);


            var vacancyList = ((VacancySearchResults) (result).Body).Results.ToList();
            return vacancyList;
        }

        private async Task<List<Result>> GetVacancyListByRoute(string routeId, int distance,
            CoordinatesResponse coordinates, int pageNumber = 1)
        {
            var standards = await _standardsService.GetByRoute(routeId);

            var standardIds = string.Join(',', standards.Select(s => s.Id.ToString()));

            var result = (HttpOperationResponse<object>) _vacanciesApi.SearchApprenticeshipVacancies(
                coordinates.Coordinates.Lat, coordinates.Coordinates.Lon, pageNumber, 250, distance, standardIds);

            var vacancyList = ((VacancySearchResults) (result).Body).Results.ToList();
            return vacancyList;
        }
    }
}