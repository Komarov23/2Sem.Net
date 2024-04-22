using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using WebApplication1.Interfaces;

namespace WebApplication1.Health
{
    public class HealthWritter : IHealthCheckResponseWriter
    {
        public Task WriteAsync(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(result);
            return httpContext.Response.WriteAsync(json);
        }
    }
}
