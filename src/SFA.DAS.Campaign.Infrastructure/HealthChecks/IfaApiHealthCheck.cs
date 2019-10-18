using Ifa.Api.Api;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
                var result = await _ifaApiClient.ApprenticeshipStandardsGet_3Async();
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
