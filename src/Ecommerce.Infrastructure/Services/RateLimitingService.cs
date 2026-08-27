namespace Ecommerce.Infrastructure.Services;

public class RateLimitingService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<RateLimitingService> _logger;

    public RateLimitingService(ICacheService cacheService, ILogger<RateLimitingService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<RateLimitResult> CheckRateLimitAsync(string key, int maxRequests, TimeSpan window)
    {
        var cacheKey = $"ratelimit:{key}:{DateTime.UtcNow:yyyyMMddHHmm}";
        var currentCount = await _cacheService.GetAsync<int>(cacheKey);

        var result = new RateLimitResult
        {
            Key = key,
            MaxRequests = maxRequests,
            Window = window,
            IsAllowed = currentCount < maxRequests,
            CurrentCount = currentCount + 1,
            Remaining = Math.Max(0, maxRequests - currentCount - 1),
            ResetAt = DateTime.UtcNow.Add(window)
        };

        if (result.IsAllowed)
        {
            await _cacheService.SetAsync(cacheKey, result.CurrentCount, window);
        }
        else
        {
            _logger.LogWarning("Rate limit exceeded for key: {Key}", key);
        }

        return result;
    }

    public async Task<bool> IsRateLimitedAsync(string key, int maxRequests, TimeSpan window)
    {
        var result = await CheckRateLimitAsync(key, maxRequests, window);
        return !result.IsAllowed;
    }

    public async Task ResetRateLimitAsync(string key)
    {
        await _cacheService.RemoveAsync($"ratelimit:{key}");
    }

    public async Task<Dictionary<string, RateLimitResult>> GetRateLimitStatusAsync(List<string> keys, int maxRequests, TimeSpan window)
    {
        var results = new Dictionary<string, RateLimitResult>();
        foreach (var key in keys)
        {
            results[key] = await CheckRateLimitAsync(key, maxRequests, window);
        }
        return results;
    }
}

public class RateLimitResult
{
    public string Key { get; set; } = string.Empty;
    public int MaxRequests { get; set; }
    public int CurrentCount { get; set; }
    public int Remaining { get; set; }
    public TimeSpan Window { get; set; }
    public bool IsAllowed { get; set; }
    public DateTime ResetAt { get; set; }
    public double RetryAfterSeconds => IsAllowed ? 0 : (ResetAt - DateTime.UtcNow).TotalSeconds;
}

public class ApiKeyService
{
    private readonly ICacheService _cacheService;

    public ApiKeyService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<ApiKeyInfo> ValidateApiKeyAsync(string apiKey)
    {
        var cacheKey = $"apikey:{apiKey}";
        var info = await _cacheService.GetAsync<ApiKeyInfo>(cacheKey);

        if (info == null)
        {
            return new ApiKeyInfo { IsValid = false };
        }

        if (info.ExpiresAt.HasValue && info.ExpiresAt < DateTime.UtcNow)
        {
            return new ApiKeyInfo { IsValid = false };
        }

        return info;
    }

    public async Task<ApiKeyInfo> CreateApiKeyAsync(string name, List<string> scopes, TimeSpan? expiry = null)
    {
        var apiKey = GenerateApiKey();
        var info = new ApiKeyInfo
        {
            Key = apiKey,
            Name = name,
            Scopes = scopes,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiry.HasValue ? DateTime.UtcNow.Add(expiry.Value) : null,
            IsValid = true
        };

        await _cacheService.SetAsync($"apikey:{apiKey}", info, expiry ?? TimeSpan.FromDays(365));
        return info;
    }

    public async Task RevokeApiKeyAsync(string apiKey)
    {
        await _cacheService.RemoveAsync($"apikey:{apiKey}");
    }

    private string GenerateApiKey()
    {
        var bytes = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
    }
}

public class ApiKeyInfo
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Scopes { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsValid { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public string? LastUsedIp { get; set; }
    public int UsageCount { get; set; }
}
