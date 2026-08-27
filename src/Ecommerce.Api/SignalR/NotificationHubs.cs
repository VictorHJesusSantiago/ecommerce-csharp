using Microsoft.AspNetCore.SignalR;

namespace Ecommerce.Api.SignalR;

public class OrderNotificationHub : Hub
{
    private readonly ILogger<OrderNotificationHub> _logger;

    public OrderNotificationHub(ILogger<OrderNotificationHub> logger) => _logger = logger;

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected to OrderNotificationHub: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public async Task JoinOrderGroup(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"order_{orderId}");
        _logger.LogInformation("Client {ConnectionId} joined order group {OrderId}", Context.ConnectionId, orderId);
    }

    public async Task LeaveOrderGroup(string orderId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"order_{orderId}");
    }

    public async Task SendOrderUpdate(string orderId, string status)
    {
        await Clients.Group($"order_{orderId}").SendAsync("OrderStatusUpdated", orderId, status);
    }

    public async Task SendNotification(string message)
    {
        await Clients.Caller.SendAsync("ReceiveNotification", message);
    }
}

public class AdminNotificationHub : Hub
{
    private readonly ILogger<AdminNotificationHub> _logger;

    public AdminNotificationHub(ILogger<AdminNotificationHub> logger) => _logger = logger;

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "admins");
        await base.OnConnectedAsync();
    }

    public async Task SendDashboardUpdate(object data)
    {
        await Clients.Group("admins").SendAsync("DashboardUpdate", data);
    }

    public async Task SendLowStockAlert(string productName, int currentStock)
    {
        await Clients.Group("admins").SendAsync("LowStockAlert", productName, currentStock);
    }
}

public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger) => _logger = logger;

    public async Task SendPersonalNotification(Guid userId, string title, string message, string? actionUrl = null)
    {
        await Clients.Group($"user_{userId}").SendAsync("PersonalNotification", title, message, actionUrl);
    }

    public async Task JoinUserGroup(Guid userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
    }
}
