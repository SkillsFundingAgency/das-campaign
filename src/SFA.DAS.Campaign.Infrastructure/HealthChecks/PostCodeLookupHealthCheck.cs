using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.Campaign.Domain.Interfaces;
using SFA.DAS.Campaign.Domain.Models.Geocode;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public class PostCodeLookupHealthCheck : IHealthCheck
    {
        private readonly IGeocodeService _postCodeService;

        public PostCodeLookupHealthCheck(IGeocodeService postCodeService)
        {
            _postCodeService = postCodeService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthCheckResultHealthy = true;

            try
            {
                var result = await _postCodeService.GetFromPostCode("SW1A 1AA"); // Buckingham Palace

                if (result.ResponseCode == LocationLookupResponse.ServerError)
                    healthCheckResultHealthy = false;
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
