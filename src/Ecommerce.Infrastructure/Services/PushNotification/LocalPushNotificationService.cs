using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalPushNotificationService : IPushNotificationService
{
    private readonly ILogger<LocalPushNotificationService> _logger;

    public LocalPushNotificationService(ILogger<LocalPushNotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendPushNotificationAsync(string userId, string title, string message, object? data = null)
    {
        _logger.LogInformation("Push notification sent to {UserId}: {Title}", userId, title);
        return Task.CompletedTask;
    }

    public Task SendBulkPushNotificationAsync(List<string> userIds, string title, string message, object? data = null)
    {
        _logger.LogInformation("Bulk push notification sent to {Count} users", userIds.Count);
        return Task.CompletedTask;
    }

    public Task RegisterDeviceAsync(string userId, string deviceToken, string platform)
    {
        _logger.LogInformation("Device registered for {UserId}: {Platform}", userId, platform);
        return Task.CompletedTask;
    }

    public Task UnregisterDeviceAsync(string userId, string deviceToken)
    {
        _logger.LogInformation("Device unregistered for {UserId}", userId);
        return Task.CompletedTask;
    }
}
