using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Campaign.Infrastructure.HealthChecks
{
    public interface IVacancyServiceApiHealthCheck : IHealthCheck
    {
        Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext healthCheckContext, CancellationToken cancellationToken);
    }
}
