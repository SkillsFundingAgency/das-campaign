using System;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;
using SFA.DAS.Campaign.Domain.ApprenticeshipCourses;
using SFA.DAS.Experiments.Application.Helpers;
using VacanciesApi;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public class VacancyServiceApiHealthCheck : IHealthCheck
    {
        private readonly ILivevacanciesAPI _vacancyApiClient;
        private readonly IStandardsRepository _standardsRepository;

        public VacancyServiceApiHealthCheck(ILivevacanciesAPI vacancyApiClient, IStandardsRepository standardsRepository)
        {
            _vacancyApiClient = vacancyApiClient;
            _standardsRepository = standardsRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;
            string reason = null;
            try
            {
                var result = _vacancyApiClient.SearchApprenticeshipVacancies(51.535125, -0.101613, 1, 1, 100);
            }
            catch
            {
                healthCheckResultHealthy = false;
                reason = "Unable to call Vacancy Api - this probably means it is not currently accepting requests";
            }


            if (healthCheckResultHealthy)
            {

                string errorResponse;
                try
                {
                    var standards = await _standardsRepository.GetAll();

                   var splitStandards = standards.SplitList(200);


                   Parallel.ForEach(splitStandards, (standardsList) =>
                   {
                       var standardIds = string.Join(',', standardsList.Select(s => s.Id.ToString()));

                       var result = (HttpOperationResponse<object>)_vacancyApiClient.SearchApprenticeshipVacancies(
                           51.4825766, -0.0076589, 1, 250, 10, standardIds);

                       if (!result.Response.IsSuccessStatusCode)
                       {
                           healthCheckResultHealthy = false;
                           reason = $"Called the vacancy APi successfully, but received an error in response, Error: {result.Response.Content.ReadAsStringAsync().Result}";
                       }

                   });


                }
                catch(Exception ex)
                {
                    healthCheckResultHealthy = false;
                    reason = $"Called the vacancy APi successfully, but received an error in response, Error: {ex.Message}";
                }
            }

            var healthcheckStatus = healthCheckResultHealthy ? HealthStatus.Healthy : HealthStatus.Unhealthy;
            return new HealthCheckResult(healthcheckStatus, reason);
       ;
        }
    }
}
