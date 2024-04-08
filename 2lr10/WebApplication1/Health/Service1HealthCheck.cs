using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.Health
{
    public class Service1HealthCheck : IHealthCheck
    {
        private readonly bool IsHealthy = true;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) => IsHealthy 
            ? Task.FromResult(HealthCheckResult.Healthy("HEALTHY")) 
            : Task.FromResult(HealthCheckResult.Unhealthy("UNHEALTHY"));
    }
}
