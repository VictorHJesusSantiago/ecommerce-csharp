namespace Ecommerce.Infrastructure.Services;

public class EventDispatcher : IEventBus
{
    private readonly IPublisher _publisher;
    private readonly ILogger<EventDispatcher> _logger;

    public EventDispatcher(IPublisher publisher, ILogger<EventDispatcher> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class
    {
        _logger.LogInformation("Publishing event {EventType}", typeof(T).Name);
        await _publisher.Publish(@event, ct);
    }

    public async Task PublishAsync(IEnumerable<object> events, CancellationToken ct = default)
    {
        foreach (var @event in events)
        {
            await PublishAsync(@event, ct);
        }
    }
}

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    string? FullName { get; }
    bool IsAuthenticated { get; }
    string? IpAddress { get; }
    string? UserAgent { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? Guid.Parse(claim.Value) : null;
        }
    }

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    public string? FullName
    {
        get
        {
            var firstName = _httpContextAccessor.HttpContext?.User?.FindFirst("FirstName")?.Value;
            var lastName = _httpContextAccessor.HttpContext?.User?.FindFirst("LastName")?.Value;
            return $"{firstName} {lastName}".Trim();
        }
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string? IpAddress => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

    public string? UserAgent => _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].FirstOrDefault();
}

public class AuditService : IAuditService
{
    private readonly EcommerceDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AuditService(EcommerceDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task LogAsync(string action, string? entityName = null, string? entityId = null,
        object? oldValue = null, object? newValue = null, CancellationToken ct = default)
    {
        var auditLog = new
        {
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = oldValue != null ? System.Text.Json.JsonSerializer.Serialize(oldValue) : null,
            NewValues = newValue != null ? System.Text.Json.JsonSerializer.Serialize(newValue) : null,
            UserId = _currentUserService.UserId,
            IpAddress = _currentUserService.IpAddress,
            UserAgent = _currentUserService.UserAgent,
            Timestamp = DateTime.UtcNow
        };

        _logger.LogInformation("Audit: {Action} on {EntityName} ({EntityId}) by {UserId}",
            action, entityName, entityId, _currentUserService.UserId);

        await Task.CompletedTask;
    }
}

public interface IAuditService
{
    Task LogAsync(string action, string? entityName = null, string? entityId = null,
        object? oldValue = null, object? newValue = null, CancellationToken ct = default);
}
