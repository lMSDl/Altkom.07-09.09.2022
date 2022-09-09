using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelathCheck
{
    public class DirectoryAccess : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if(Directory.Exists("c:\\CustomService"))
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("Dir missing"));
        }
    }
}
