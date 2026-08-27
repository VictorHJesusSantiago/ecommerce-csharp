using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class NullEventBus : IEventBus
{
    private readonly ILogger<NullEventBus> _logger;

    public NullEventBus(ILogger<NullEventBus> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
    {
        _logger.LogInformation("Event published (in-memory): {EventType}", typeof(T).Name);
        return Task.CompletedTask;
    }

    public Task SubscribeAsync<T>(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default) where T : class
    {
        _logger.LogInformation("Handler subscribed for event: {EventType}", typeof(T).Name);
        return Task.CompletedTask;
    }
}
