namespace Ecommerce.Application.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);
    Task SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, CancellationToken cancellationToken = default);
    Task SendTemplatedEmailAsync(string to, string templateId, Dictionary<string, string> parameters, CancellationToken cancellationToken = default);
    Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);
}

public interface ISmsService
{
    Task SendSmsAsync(string to, string message, CancellationToken cancellationToken = default);
    Task SendBulkSmsAsync(List<string> recipients, string message, CancellationToken cancellationToken = default);
}

public interface IPushNotificationService
{
    Task SendPushNotificationAsync(Guid userId, string title, string body, string? imageUrl = null, string? actionUrl = null, CancellationToken cancellationToken = default);
    Task SendBulkPushNotificationAsync(List<Guid> userIds, string title, string body, string? imageUrl = null, CancellationToken cancellationToken = default);
}

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken cancellationToken = default);
    Task<string> GetPresignedUrlAsync(string key, int expirationMinutes = 60, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string key, CancellationToken cancellationToken = default);
    Task<bool> FileExistsAsync(string key, CancellationToken cancellationToken = default);
}

public interface IShippingCalculatorService
{
    Task<List<ShippingRateResult>> CalculateShippingRatesAsync(decimal weight, string countryCode, decimal orderTotal, CancellationToken cancellationToken = default);
    Task<string?> GetTrackingInfoAsync(string trackingNumber, string carrier, CancellationToken cancellationToken = default);
}

public record ShippingRateResult
{
    public string Method { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public int EstimatedDaysMin { get; init; }
    public int EstimatedDaysMax { get; init; }
    public string Description { get; init; } = string.Empty;
}

public interface IExchangeRateService
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken cancellationToken = default);
    Task<Dictionary<string, decimal>> GetAllRatesAsync(string baseCurrency, CancellationToken cancellationToken = default);
}

public interface IGeoLocationService
{
    Task<GeoLocationResult> GetLocationByIpAsync(string ipAddress, CancellationToken cancellationToken = default);
    Task<string> GetCountryByPostalCodeAsync(string postalCode, string countryCode, CancellationToken cancellationToken = default);
}

public record GeoLocationResult
{
    public string? Country { get; init; }
    public string? City { get; init; }
    public string? Region { get; init; }
    public string? PostalCode { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public string? TimeZone { get; init; }
}

public interface IWebhookService
{
    Task SendWebhookAsync(string url, object payload, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default);
    Task SendWebhookWithRetryAsync(string url, object payload, int maxRetries = 3, CancellationToken cancellationToken = default);
}

public interface IBotDetectionService
{
    Task<bool> IsBotAsync(string userAgent, CancellationToken cancellationToken = default);
    Task<bool> IsSuspiciousRequestAsync(string ipAddress, string userAgent, CancellationToken cancellationToken = default);
}

public interface ICurrencyConversionService
{
    Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency, CancellationToken cancellationToken = default);
    Task<string> GetCurrencySymbolAsync(string currencyCode, CancellationToken cancellationToken = default);
}

public interface IQueueService
{
    Task EnqueueAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : class;
    Task<T?> DequeueAsync<T>(string queueName, CancellationToken cancellationToken = default) where T : class;
    Task<int> GetQueueCountAsync(string queueName, CancellationToken cancellationToken = default);
}

public interface IIdempotencyService
{
    Task<bool> IsIdempotentRequestAsync(string idempotencyKey, CancellationToken cancellationToken = default);
    Task StoreIdempotentRequestAsync(string idempotencyKey, object response, TimeSpan? expiry = null, CancellationToken cancellationToken = default);
    Task<T?> GetIdempotentResponseAsync<T>(string idempotencyKey, CancellationToken cancellationToken = default);
}
