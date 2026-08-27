using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Catalog;

namespace Ecommerce.Application.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken ct = default);
    Task SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, CancellationToken ct = default);
    Task SendTemplatedEmailAsync(string to, string templateId, Dictionary<string, string> parameters, CancellationToken ct = default);
    Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true, CancellationToken ct = default);
}

public interface ISmsService
{
    Task SendSmsAsync(string to, string message, CancellationToken ct = default);
    Task SendBulkSmsAsync(List<string> recipients, string message, CancellationToken ct = default);
}

public interface IPushNotificationService
{
    Task SendPushNotificationAsync(Guid userId, string title, string body, string? imageUrl = null, string? actionUrl = null, CancellationToken ct = default);
    Task SendBulkPushNotificationAsync(List<Guid> userIds, string title, string body, string? imageUrl = null, CancellationToken ct = default);
}

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken ct = default);
    Task<string> GetPresignedUrlAsync(string key, int expirationMinutes = 60, CancellationToken ct = default);
    Task DeleteFileAsync(string key, CancellationToken ct = default);
    Task<bool> FileExistsAsync(string key, CancellationToken ct = default);
}

public interface IShippingCalculatorService
{
    decimal CalculateShippingCost(decimal subtotal, decimal weight, string destination);
    int GetEstimatedDeliveryDays(string method, string destination);
    bool IsEligibleForFreeShipping(decimal subtotal);
}

public interface IExchangeRateService
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken ct = default);
    Task<Dictionary<string, decimal>> GetAllRatesAsync(string baseCurrency, CancellationToken ct = default);
}

public interface ICurrencyConversionService
{
    Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency, CancellationToken ct = default);
    Task<string> GetCurrencySymbolAsync(string currencyCode, CancellationToken ct = default);
}

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateRandomString(int length);
    string GenerateApiKey();
}

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken ct = default) where T : class;
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken ct = default) where T : class;
    Task RemoveAsync(string key, CancellationToken ct = default);
    Task RemoveByPatternAsync(string pattern, CancellationToken ct = default);
    Task<bool> ExistsAsync(string key, CancellationToken ct = default);
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken ct = default) where T : class;
}

public interface IMessageQueueService
{
    Task PublishAsync<T>(T message, string queueName, CancellationToken ct = default) where T : class;
    Task SubscribeAsync<T>(string queueName, Func<T, Task> handler, CancellationToken ct = default) where T : class;
}

public interface IIdempotencyService
{
    Task<bool> IsIdempotentRequestAsync(string idempotencyKey, CancellationToken ct = default);
    Task StoreIdempotentRequestAsync(string idempotencyKey, object response, TimeSpan? expiry = null, CancellationToken ct = default);
    Task<T?> GetIdempotentResponseAsync<T>(string idempotencyKey, CancellationToken ct = default) where T : class;
}

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class;
}

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    IEnumerable<string> Roles { get; }
}

public interface IAuditService
{
    Task LogActivityAsync(string action, string? entityType = null, Guid? entityId = null, string? details = null, CancellationToken ct = default);
}
