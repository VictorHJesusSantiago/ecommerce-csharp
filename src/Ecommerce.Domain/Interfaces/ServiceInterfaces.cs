using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Domain.Interfaces;

public interface IProductSearchService
{
    Task<IReadOnlyList<Product>> SearchAsync(string query, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetSuggestionsAsync(string query, int maxSuggestions = 10, CancellationToken cancellationToken = default);
    Task IndexProductAsync(Product product, CancellationToken cancellationToken = default);
    Task RemoveProductAsync(Guid productId, CancellationToken cancellationToken = default);
    Task ReindexAllAsync(CancellationToken cancellationToken = default);
}

public interface IImageService
{
    Task<string> UploadImageAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken cancellationToken = default);
    Task<string> GetPresignedUrlAsync(string key, int expirationMinutes = 60, CancellationToken cancellationToken = default);
    Task DeleteImageAsync(string key, CancellationToken cancellationToken = default);
    Task<string> ResizeImageAsync(string imageUrl, int width, int height, CancellationToken cancellationToken = default);
}

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class;
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class;
}

public interface IMessageQueueService
{
    Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : class;
    Task SubscribeAsync<T>(string queueName, Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class;
}

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class;
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

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
    IReadOnlyList<string> GetRoles();
}

public interface IReportService
{
    Task<byte[]> GenerateSalesReportAsync(DateTime fromDate, DateTime toDate, string format = "PDF", CancellationToken cancellationToken = default);
    Task<byte[]> GenerateInventoryReportAsync(CancellationToken cancellationToken = default);
    Task<byte[]> GenerateCustomerReportAsync(DateTime fromDate, DateTime toDate, string format = "PDF", CancellationToken cancellationToken = default);
    Task<byte[]> GenerateProductPerformanceReportAsync(DateTime fromDate, DateTime toDate, string format = "PDF", CancellationToken cancellationToken = default);
}

public interface IExportService
{
    Task<string> ExportOrdersToCsvAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<string> ExportProductsToCsvAsync(CancellationToken cancellationToken = default);
    Task<string> ExportCustomersToCsvAsync(CancellationToken cancellationToken = default);
}

public interface ICurrencyService
{
    decimal Convert(decimal amount, string fromCurrency, string toCurrency);
    string GetCurrencySymbol(string currencyCode);
    decimal GetExchangeRate(string fromCurrency, string toCurrency);
}
