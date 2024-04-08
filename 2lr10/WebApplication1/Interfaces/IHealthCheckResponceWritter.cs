using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1.Interfaces
{
    public interface IHealthCheckResponseWriter
    {
        Task WriteAsync(HttpContext httpContext, HealthReport result);
    }
}
