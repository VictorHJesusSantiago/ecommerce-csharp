using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class InMemoryIdempotencyService : IIdempotencyService
{
    private readonly Dictionary<string, (object Response, DateTime Expiry)> _store = new();
    private readonly ILogger<InMemoryIdempotencyService> _logger;

    public InMemoryIdempotencyService(ILogger<InMemoryIdempotencyService> logger)
    {
        _logger = logger;
    }

    public Task<T?> GetResponseAsync<T>(string key) where T : class
    {
        lock (_store)
        {
            if (_store.TryGetValue(key, out var entry) && entry.Expiry > DateTime.UtcNow)
            {
                _logger.LogDebug("Idempotency key found: {Key}", key);
                return Task.FromResult(entry.Response as T);
            }
            _store.Remove(key);
        }
        return Task.FromResult<T?>(null);
    }

    public Task SetResponseAsync<T>(string key, T response, TimeSpan expiry) where T : class
    {
        lock (_store)
        {
            _store[key] = (response, DateTime.UtcNow.Add(expiry));
            _logger.LogDebug("Idempotency key set: {Key}", key);
        }
        return Task.CompletedTask;
    }

    public Task<bool> HasKeyAsync(string key)
    {
        lock (_store)
        {
            return Task.FromResult(_store.ContainsKey(key) && _store[key].Expiry > DateTime.UtcNow);
        }
    }

    public Task RemoveKeyAsync(string key)
    {
        lock (_store) { _store.Remove(key); }
        return Task.CompletedTask;
    }
}
