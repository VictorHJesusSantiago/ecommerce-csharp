using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalApiKeyService : IApiKeyService
{
    private readonly Dictionary<string, ApiKeyInfo> _keys = new();
    private readonly ILogger<LocalApiKeyService> _logger;

    public LocalApiKeyService(ILogger<LocalApiKeyService> logger)
    {
        _logger = logger;
    }

    public string GenerateApiKey(string name, string[]? scopes = null)
    {
        var key = $"ak_{Guid.NewGuid():N}";
        _keys[key] = new ApiKeyInfo
        {
            Key = key,
            Name = name,
            Scopes = scopes ?? [],
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        _logger.LogInformation("API key generated for {Name}", name);
        return key;
    }

    public bool ValidateApiKey(string key, string requiredScope)
    {
        if (!_keys.TryGetValue(key, out var info) || !info.IsActive)
            return false;
        if (info.ExpiresAt.HasValue && info.ExpiresAt.Value < DateTime.UtcNow)
            return false;
        return info.Scopes.Contains(requiredScope) || info.Scopes.Contains("*");
    }

    public ApiKeyInfo? GetApiKeyInfo(string key)
    {
        _keys.TryGetValue(key, out var info);
        return info;
    }

    public void RevokeApiKey(string key)
    {
        if (_keys.TryGetValue(key, out var info))
        {
            info.IsActive = false;
            _logger.LogInformation("API key revoked: {Name}", info.Name);
        }
    }
}

public interface IApiKeyService
{
    string GenerateApiKey(string name, string[]? scopes = null);
    bool ValidateApiKey(string key, string requiredScope);
    ApiKeyInfo? GetApiKeyInfo(string key);
    void RevokeApiKey(string key);
}

public class ApiKeyInfo
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; }
}
