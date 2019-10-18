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

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;

            try
            {
                var result = _vacancyApiClient.SearchApprenticeshipVacancies(51.535125, -0.101613, 1, 1, 100);
            }
            catch
            {
                healthCheckResultHealthy = false;
            }

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
