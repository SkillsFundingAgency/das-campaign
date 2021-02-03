using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using VacanciesApi;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public class VacancyServiceApiHealthCheck : IHealthCheck
    {
        private readonly ILivevacanciesAPI _vacancyApiClient;

        public VacancyServiceApiHealthCheck(ILivevacanciesAPI vacancyApiClient)
        {
            _vacancyApiClient = vacancyApiClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;
            string reason = null;
            try
            {
                _vacancyApiClient.SearchApprenticeshipVacancies(51.535125, -0.101613, 1, 1, 100);
            }
            catch
            {
                healthCheckResultHealthy = false;
                reason = "Unable to call Vacancy Api - this probably means it is not currently accepting requests";
            }

            var healthcheckStatus = healthCheckResultHealthy ? HealthStatus.Healthy : HealthStatus.Unhealthy;
            return new HealthCheckResult(healthcheckStatus, reason);
       ;
        }

    }
}
