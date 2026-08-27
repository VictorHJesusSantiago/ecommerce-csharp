using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Ecommerce.Infrastructure.Services;

public class LocalRateLimitingService : IRateLimitingService
{
    private readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _clients = new();
    private readonly ILogger<LocalRateLimitingService> _logger;
    private readonly int _maxRequests;
    private readonly TimeSpan _window;

    public LocalRateLimitingService(ILogger<LocalRateLimitingService> logger, int maxRequests = 100, int windowSeconds = 60)
    {
        _logger = logger;
        _maxRequests = maxRequests;
        _window = TimeSpan.FromSeconds(windowSeconds);
    }

    public bool IsAllowed(string key)
    {
        var now = DateTime.UtcNow;
        var entry = _clients.AddOrUpdate(key, (1, now), (k, existing) =>
        {
            if (now - existing.WindowStart > _window)
                return (1, now);
            if (existing.Count >= _maxRequests)
                return existing;
            return (existing.Count + 1, existing.WindowStart);
        });

        var allowed = entry.Count <= _maxRequests;
        if (!allowed)
        {
            _logger.LogWarning("Rate limit exceeded for key: {Key}", key);
        }
        return allowed;
    }

    public int GetRemainingRequests(string key)
    {
        if (!_clients.TryGetValue(key, out var entry))
            return _maxRequests;
        if (DateTime.UtcNow - entry.WindowStart > _window)
            return _maxRequests;
        return Math.Max(0, _maxRequests - entry.Count);
    }

    public TimeSpan GetRetryAfter(string key)
    {
        if (!_clients.TryGetValue(key, out var entry))
            return TimeSpan.Zero;
        var elapsed = DateTime.UtcNow - entry.WindowStart;
        if (elapsed >= _window)
            return TimeSpan.Zero;
        return _window - elapsed;
    }
}

public interface IRateLimitingService
{
    bool IsAllowed(string key);
    int GetRemainingRequests(string key);
    TimeSpan GetRetryAfter(string key);
}
