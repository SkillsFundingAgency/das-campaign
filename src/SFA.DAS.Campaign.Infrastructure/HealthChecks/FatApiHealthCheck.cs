using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.Apprenticeships.Api.Client;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public class FatApiHealthCheck : IHealthCheck
    {
        private readonly IApprenticeshipProgrammeApiClient _fatApiClient;

        public FatApiHealthCheck(IApprenticeshipProgrammeApiClient fatApiClient)
        {
            _fatApiClient = fatApiClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;

            try
            {
                var result = await _fatApiClient.SearchAsync("zzzsxsx"); // Not checking that it returns values. Just that it doesn't error.
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
