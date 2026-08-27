using SendGrid;
using SendGrid.Helpers.Mail;
using Ecommerce.Application.Contracts;

namespace Ecommerce.Infrastructure.Notification;

public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _client;
    private readonly SmtpSettings _settings;
    private readonly ILogger<SendGridEmailService> _logger;

    public SendGridEmailService(ISendGridClient client, IOptions<SmtpSettings> settings, ILogger<SendGridEmailService> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        await SendEmailAsync(to, subject, body, null, null, isHtml, cancellationToken);
    }

    public async Task SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        try
        {
            var from = new EmailAddress(fromEmail ?? _settings.FromEmail, fromName ?? _settings.FromName);
            var toAddress = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toAddress, subject,
                isHtml ? null : body, isHtml ? body : null);

            var response = await _client.SendEmailAsync(msg, cancellationToken);
            _logger.LogInformation("Email sent to {To}: {Subject} - Status: {StatusCode}",
                to, subject, response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}: {Subject}", to, subject);
            throw;
        }
    }

    public async Task SendTemplatedEmailAsync(string to, string templateId, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
    {
        try
        {
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var toAddress = new EmailAddress(to);
            var templateData = parameters.Cast<object>().ToDictionary(k => k.Key, v => (object)v.Value);
            var msg = MailHelper.CreateSingleTemplateEmail(from, toAddress, templateId, templateData);

            await _client.SendEmailAsync(msg, cancellationToken);
            _logger.LogInformation("Templated email sent to {To}: Template {TemplateId}", to, templateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send templated email to {To}", to);
            throw;
        }
    }

    public async Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        try
        {
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var messages = recipients.Select(r => MailHelper.CreateSingleEmail(
                from, new EmailAddress(r), subject, isHtml ? null : body, isHtml ? body : null)).ToList();

            var response = await _client.SendMultipleAsync(messages, cancellationToken);
            _logger.LogInformation("Bulk email sent to {Count} recipients: {StatusCode}", recipients.Count, response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send bulk email to {Count} recipients", recipients.Count);
            throw;
        }
    }
}

public class ConsoleEmailService : IEmailService
{
    private readonly ILogger<ConsoleEmailService> _logger;

    public ConsoleEmailService(ILogger<ConsoleEmailService> logger) => _logger = logger;

    public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[EMAIL] To: {To} | Subject: {Subject}", to, subject);
        return Task.CompletedTask;
    }

    public Task SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[EMAIL] To: {To} | From: {From} | Subject: {Subject}", to, fromEmail ?? "noreply@ecommerce.com", subject);
        return Task.CompletedTask;
    }

    public Task SendTemplatedEmailAsync(string to, string templateId, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[EMAIL] To: {To} | Template: {TemplateId}", to, templateId);
        return Task.CompletedTask;
    }

    public Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[EMAIL] Bulk to {Count} recipients | Subject: {Subject}", recipients.Count, subject);
        return Task.CompletedTask;
    }
}

public class ConsoleSmsService : ISmsService
{
    private readonly ILogger<ConsoleSmsService> _logger;

    public ConsoleSmsService(ILogger<ConsoleSmsService> logger) => _logger = logger;

    public Task SendSmsAsync(string to, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[SMS] To: {To} | Message: {Message}", to, message);
        return Task.CompletedTask;
    }

    public Task SendBulkSmsAsync(List<string> recipients, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[SMS] Bulk to {Count} recipients", recipients.Count);
        return Task.CompletedTask;
    }
}

public class ConsolePushNotificationService : IPushNotificationService
{
    private readonly ILogger<ConsolePushNotificationService> _logger;

    public ConsolePushNotificationService(ILogger<ConsolePushNotificationService> logger) => _logger = logger;

    public Task SendPushNotificationAsync(Guid userId, string title, string body, string? imageUrl = null, string? actionUrl = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[PUSH] User: {UserId} | Title: {Title}", userId, title);
        return Task.CompletedTask;
    }

    public Task SendBulkPushNotificationAsync(List<Guid> userIds, string title, string body, string? imageUrl = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[PUSH] Bulk to {Count} users | Title: {Title}", userIds.Count, title);
        return Task.CompletedTask;
    }
}

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IConfiguration configuration, ILogger<LocalFileStorageService> logger)
    {
        _basePath = configuration["FileStorage:BasePath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        _logger = logger;
        Directory.CreateDirectory(_basePath);
    }

    public async Task<string> UploadFileAsync(Stream stream, string fileName, string contentType, string? folder = null, CancellationToken cancellationToken = default)
    {
        var directory = Path.Combine(_basePath, folder ?? "general");
        Directory.CreateDirectory(directory);

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(directory, uniqueFileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream, cancellationToken);

        _logger.LogInformation("File uploaded: {FilePath}", filePath);
        return $"/uploads/{folder ?? "general"}/{uniqueFileName}";
    }

    public Task<string> GetPresignedUrlAsync(string key, int expirationMinutes = 60, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(key);
    }

    public Task DeleteFileAsync(string key, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_basePath, key.TrimStart('/'));
        if (File.Exists(filePath))
            File.Delete(filePath);
        return Task.CompletedTask;
    }

    public Task<bool> FileExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_basePath, key.TrimStart('/'));
        return Task.FromResult(File.Exists(filePath));
    }
}

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly ILogger<CurrencyConversionService> _logger;
    private readonly Dictionary<string, decimal> _exchangeRates = new()
    {
        ["USD"] = 1.0m,
        ["EUR"] = 0.92m,
        ["GBP"] = 0.79m,
        ["JPY"] = 149.50m,
        ["CAD"] = 1.36m,
        ["AUD"] = 1.53m,
        ["CHF"] = 0.88m,
        ["CNY"] = 7.24m,
        ["INR"] = 83.12m,
        ["MXN"] = 17.15m
    };

    public CurrencyConversionService(ILogger<CurrencyConversionService> logger) => _logger = logger;

    public Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency, CancellationToken cancellationToken = default)
    {
        if (!_exchangeRates.TryGetValue(fromCurrency.ToUpperInvariant(), out var fromRate))
            throw new ArgumentException($"Unsupported currency: {fromCurrency}");
        if (!_exchangeRates.TryGetValue(toCurrency.ToUpperInvariant(), out var toRate))
            throw new ArgumentException($"Unsupported currency: {toCurrency}");

        var amountInUsd = amount / fromRate;
        var result = amountInUsd * toRate;
        return Task.FromResult(Math.Round(result, 2));
    }

    public Task<string> GetCurrencySymbolAsync(string currencyCode, CancellationToken cancellationToken = default)
    {
        var symbol = currencyCode.ToUpperInvariant() switch
        {
            "USD" => "$",
            "EUR" => "\u20AC",
            "GBP" => "\u00A3",
            "JPY" => "\u00A5",
            "CAD" => "CA$",
            "AUD" => "A$",
            "CHF" => "CHF",
            "CNY" => "\u00A5",
            "INR" => "\u20B9",
            "MXN" => "MX$",
            _ => currencyCode
        };
        return Task.FromResult(symbol);
    }
}

public class IdempotencyService : IIdempotencyService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<IdempotencyService> _logger;

    public IdempotencyService(ICacheService cacheService, ILogger<IdempotencyService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<bool> IsIdempotentRequestAsync(string idempotencyKey, CancellationToken cancellationToken = default)
    {
        return await _cacheService.ExistsAsync($"idempotent:{idempotencyKey}", cancellationToken);
    }

    public async Task StoreIdempotentRequestAsync(string idempotencyKey, object response, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        await _cacheService.SetAsync($"idempotent:{idempotencyKey}", response, expiry ?? TimeSpan.FromHours(24), cancellationToken);
    }

    public async Task<T?> GetIdempotentResponseAsync<T>(string idempotencyKey, CancellationToken cancellationToken = default) where T : class
    {
        return await _cacheService.GetAsync<T>($"idempotent:{idempotencyKey}", cancellationToken);
    }
}
