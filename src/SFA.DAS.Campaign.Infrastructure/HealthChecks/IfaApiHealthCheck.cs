using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Campaign.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public class IfaApiHealthCheck : IHealthCheck
    {
        private readonly IApprenticeshipStandardsApi _ifaApiClient;

        public IfaApiHealthCheck(IApprenticeshipStandardsApi ifaApiClient)
        {
            _ifaApiClient = ifaApiClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;

            try
            {
                var result = await _ifaApiClient.GetAllStandardsAsync();

                if (result == null)
                    healthCheckResultHealthy = false; // Expecting results.
            }
            catch
            {
                healthCheckResultHealthy = false;
            }

            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }
}
