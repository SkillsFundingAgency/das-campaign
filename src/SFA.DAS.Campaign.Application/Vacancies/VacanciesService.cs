using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Rest;
using Polly.Caching;
using SFA.DAS.Apprenticeships.Api.Client;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Campaign.Domain.Geocode;
using SFA.DAS.Campaign.Domain.Vacancies;
using SFA.DAS.Campaign.Models.ApprenticeshipCourses;
using SFA.DAS.Campaign.Models.Geocode;
using SFA.DAS.Campaign.Models.Vacancy;
using SFA.DAS.Vacancies.Api.Models;
using VacanciesApi;

namespace SFA.DAS.Campaign.Application.Vacancies
{
    public class VacanciesService : IVacanciesService
    {
        private readonly ILivevacanciesAPI _vacanciesApi;
        private readonly IVacanciesMapper _vacanciesMapper;
        private readonly IGeocodeService _geocodeService;

        public VacanciesService(ILivevacanciesAPI vacanciesApi, IVacanciesMapper vacanciesMapper, IGeocodeService geocodeService)
        {
            _vacanciesApi = vacanciesApi;
            _vacanciesMapper = vacanciesMapper;
            _geocodeService = geocodeService;
        }

        public async Task<IList<VacancySearchResultItem>> GetByPostcode(string postcode, int distance)
        {

            var coordinates = await _geocodeService.GetFromPostCode(postcode);

            int pageNumber = 1;
            var vacancyList = GetVacancyList(distance, coordinates, 1);

            
            while (vacancyList.Max(s => s.DistanceInMiles < distance))
            {
                pageNumber++;

                var list = GetVacancyList(distance, coordinates, pageNumber);
                vacancyList.AddRange(list);

                if (list.Count < 250)
                {
                   break;
                }
                
            }
            return vacancyList.Where(w => w. TrainingType == TrainingType.Standard)
                .Select(_vacanciesMapper.Map)
                .ToList();
        }

        private List<Result> GetVacancyList(int distance, CoordinatesResponse coordinates, int pageNumber = 1)
        {
            var result = (HttpOperationResponse<object>) _vacanciesApi.SearchApprenticeshipVacancies(
                latitude: coordinates.Coordinates.Lat, longitude: coordinates.Coordinates.Lon, distanceInMiles: distance,
                pageSize: 250, pageNumber: pageNumber);


            var vacancyList = ((VacancySearchResults) (result).Body).Results.ToList();
            return vacancyList;
        }
    }
}