using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce.Api.HealthChecks;

public class CacheHealthCheck : IHealthCheck
{
    private readonly Ecommerce.Application.Contracts.ICacheService _cacheService;

    public CacheHealthCheck(Ecommerce.Application.Contracts.ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _cacheService.SetAsync("health_check", "ok", TimeSpan.FromSeconds(10), cancellationToken);
            var result = await _cacheService.GetAsync<string>("health_check", cancellationToken);
            if (result == "ok")
                return HealthCheckResult.Healthy("Cache is working.");
            return HealthCheckResult.Unhealthy("Cache returned unexpected value.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Cache check failed.", ex);
        }
    }
}

public class MemoryHealthCheck : IHealthCheck
{
    private readonly IMemoryCache _cache;

    public MemoryHealthCheck(IMemoryCache cache) => _cache = cache;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Memory cache is available."));
    }
}
