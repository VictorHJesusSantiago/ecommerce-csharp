namespace Ecommerce.Infrastructure.Services;

public class WebhookService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WebhookService> _logger;
    private readonly ICacheService _cacheService;

    public WebhookService(HttpClient httpClient, ILogger<WebhookService> logger, ICacheService cacheService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _cacheService = cacheService;
    }

    public async Task<WebhookResponse> SendWebhookAsync(string url, object payload, string? secret = null, int maxRetries = 3)
    {
        var response = new WebhookResponse { Url = url, Success = false };

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync(url, content);
                response.StatusCode = (int)httpResponse.StatusCode;
                response.Body = await httpResponse.Content.ReadAsStringAsync();
                response.Success = httpResponse.IsSuccessStatusCode;

                if (response.Success) break;

                _logger.LogWarning("Webhook delivery failed (attempt {Attempt}/{MaxRetries}): {StatusCode}",
                    i + 1, maxRetries, response.StatusCode);

                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Webhook delivery error (attempt {Attempt}/{MaxRetries})", i + 1, maxRetries);
                response.ErrorMessage = ex.Message;

                if (i < maxRetries - 1)
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, i)));
            }
        }

        return response;
    }

    public async Task<WebhookResponse> SendWebhookWithRetryAsync(string url, object payload, string? secret = null)
    {
        return await SendWebhookAsync(url, payload, secret, 5);
    }

    public bool VerifyWebhookSignature(string payload, string signature, string secret)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload));
        var computedSignature = Convert.ToBase64String(hash);
        return computedSignature == signature;
    }

    public async Task<List<WebhookLog>> GetWebhookLogsAsync(string url, int limit = 50)
    {
        var cacheKey = $"webhook:logs:{url}";
        var cached = await _cacheService.GetAsync<List<WebhookLog>>(cacheKey);
        return cached ?? [];
    }
}

public class WebhookResponse
{
    public string Url { get; set; } = string.Empty;
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Body { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class WebhookLog
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public string? Response { get; set; }
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public int RetryCount { get; set; }
    public DateTime Timestamp { get; set; }
}

public class WebhookSubscription
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string? Secret { get; set; }
    public bool IsActive { get; set; }
    public List<string> Events { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
