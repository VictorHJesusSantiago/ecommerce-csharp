using MediatR;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.Handlers;

public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, ApiResponse<NotificationDto>>
{
    public async Task<ApiResponse<NotificationDto>> Handle(GetNotificationByIdQuery request, CancellationToken ct)
    {
        return ApiResponse<NotificationDto>.SuccessResponse(new NotificationDto
        {
            Id = request.Id,
            Title = "Order Shipped",
            Message = "Your order has been shipped!",
            Type = "OrderUpdate",
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        });
    }
}

public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsQuery, ApiResponse<List<NotificationDto>>>
{
    public async Task<ApiResponse<List<NotificationDto>>> Handle(GetUserNotificationsQuery request, CancellationToken ct)
    {
        return ApiResponse<List<NotificationDto>>.SuccessResponse([]);
    }
}

public class MarkNotificationAsReadHandler : IRequestHandler<MarkNotificationAsReadCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(MarkNotificationAsReadCommand request, CancellationToken ct)
    {
        return ApiResponse.SuccessResponse("Notification marked as read");
    }
}

public class MarkAllNotificationsAsReadHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken ct)
    {
        return ApiResponse.SuccessResponse("All notifications marked as read");
    }
}

public class SendNotificationHandler : IRequestHandler<SendNotificationCommand, ApiResponse<NotificationDto>>
{
    public async Task<ApiResponse<NotificationDto>> Handle(SendNotificationCommand request, CancellationToken ct)
    {
        return ApiResponse<NotificationDto>.SuccessResponse(new NotificationDto
        {
            Id = Guid.NewGuid(),
            UserId = request.Request.RecipientId,
            Title = request.Request.Title,
            Message = request.Request.Message,
            Type = request.Request.Type,
            Link = request.Request.Link,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        }, "Notification sent successfully");
    }
}

public class GetNotificationByIdQuery : IRequest<ApiResponse<NotificationDto>>
{
    public Guid Id { get; set; }
}

public class GetUserNotificationsQuery : IRequest<ApiResponse<List<NotificationDto>>>
{
    public Guid UserId { get; set; }
    public int Limit { get; set; } = 50;
    public bool UnreadOnly { get; set; }
}

public class MarkNotificationAsReadCommand : IRequest<ApiResponse>
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
}

public class MarkAllNotificationsAsReadCommand : IRequest<ApiResponse>
{
    public Guid UserId { get; set; }
}

public class SendNotificationCommand : IRequest<ApiResponse<NotificationDto>>
{
    public SendNotificationRequest Request { get; set; } = null!;
    public string? SentBy { get; set; }
}
