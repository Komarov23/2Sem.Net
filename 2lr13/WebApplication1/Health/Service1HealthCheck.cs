using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace WebApplication1.Health
{
    public class Service1HealthCheck : IHealthCheck
    {
        private readonly bool IsHealthy = true;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Log.Information("Service1HealthCheck logged in at {Time}", DateTime.Now);
            return IsHealthy
                ? Task.FromResult(HealthCheckResult.Healthy("HEALTHY"))
                : Task.FromResult(HealthCheckResult.Unhealthy("UNHEALTHY"));
        }
    }
}
