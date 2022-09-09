using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelathCheck
{
    public class RandomHealth : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var value = new Random().Next();

            if(value % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            if(value % 3 == 0)
            {
                return Task.FromResult(HealthCheckResult.Degraded("I need help..."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("..."));
        }
    }
}
