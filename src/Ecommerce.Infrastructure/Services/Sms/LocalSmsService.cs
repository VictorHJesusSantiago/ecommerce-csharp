using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalSmsService : ISmsService
{
    private readonly ILogger<LocalSmsService> _logger;

    public LocalSmsService(ILogger<LocalSmsService> logger)
    {
        _logger = logger;
    }

    public Task SendSmsAsync(string phoneNumber, string message)
    {
        _logger.LogInformation("SMS sent to {Phone}: {Message}", phoneNumber, message);
        return Task.CompletedTask;
    }

    public Task SendBulkSmsAsync(List<string> phoneNumbers, string message)
    {
        _logger.LogInformation("Bulk SMS sent to {Count} numbers", phoneNumbers.Count);
        return Task.CompletedTask;
    }

    public Task SendVerificationCodeAsync(string phoneNumber, string code)
    {
        _logger.LogInformation("Verification code sent to {Phone}: {Code}", phoneNumber, code);
        return Task.CompletedTask;
    }
}
