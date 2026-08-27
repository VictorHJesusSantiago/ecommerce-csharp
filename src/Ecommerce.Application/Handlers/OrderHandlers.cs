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

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, ApiResponse<OrderDto>>
{
    private readonly IOrderService _orderService;
    public PlaceOrderHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse<OrderDto>> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        return await _orderService.PlaceOrderAsync(request.UserId, request.Request, ct);
    }
}

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderDto>>
{
    private readonly IOrderService _orderService;
    public GetOrderByIdHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        return await _orderService.GetOrderByIdAsync(request.Id, ct);
    }
}

public class GetMyOrdersHandler : IRequestHandler<GetMyOrdersQuery, ApiResponse<PagedResponse<OrderDto>>>
{
    private readonly IOrderService _orderService;
    public GetMyOrdersHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse<PagedResponse<OrderDto>>> Handle(GetMyOrdersQuery request, CancellationToken ct)
    {
        return await _orderService.GetOrdersAsync(request.SearchRequest, ct);
    }
}

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, ApiResponse>
{
    private readonly IOrderService _orderService;
    public CancelOrderHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse> Handle(CancelOrderCommand request, CancellationToken ct)
    {
        return await _orderService.CancelOrderAsync(request.OrderId, request.Reason, request.UserId?.ToString(), ct);
    }
}

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, ApiResponse<OrderDto>>
{
    private readonly IOrderService _orderService;
    public UpdateOrderStatusHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse<OrderDto>> Handle(UpdateOrderStatusCommand request, CancellationToken ct)
    {
        return await _orderService.UpdateOrderStatusAsync(request.OrderId, request.Request, request.UpdatedBy, ct);
    }
}

public class GetOrderStatsHandler : IRequestHandler<GetOrderStatsQuery, ApiResponse<OrderStatsDto>>
{
    private readonly IOrderService _orderService;
    public GetOrderStatsHandler(IOrderService orderService) => _orderService = orderService;
    public async Task<ApiResponse<OrderStatsDto>> Handle(GetOrderStatsQuery request, CancellationToken ct)
    {
        var result = await _orderService.GetOrdersAsync(request.SearchRequest, ct);
        return ApiResponse<OrderStatsDto>.SuccessResponse(new OrderStatsDto
        {
            TotalOrders = result.Data?.TotalRecords ?? 0,
            TotalRevenue = result.Data?.Data?.Sum(o => o.TotalAmount) ?? 0,
            AverageOrderValue = result.Data?.Data?.Any() == true ? result.Data.Data.Average(o => o.TotalAmount) : 0
        });
    }
}

public class PlaceOrderCommand : IRequest<ApiResponse<OrderDto>>
{
    public Guid UserId { get; set; }
    public PlaceOrderRequest Request { get; set; } = null!;
}

public class GetOrderByIdQuery : IRequest<ApiResponse<OrderDto>>
{
    public Guid Id { get; set; }
}

public class GetMyOrdersQuery : IRequest<ApiResponse<PagedResponse<OrderDto>>>
{
    public OrderSearchRequest SearchRequest { get; set; } = null!;
}

public class CancelOrderCommand : IRequest<ApiResponse>
{
    public Guid OrderId { get; set; }
    public string? Reason { get; set; }
    public Guid? UserId { get; set; }
}

public class UpdateOrderStatusCommand : IRequest<ApiResponse<OrderDto>>
{
    public Guid OrderId { get; set; }
    public UpdateOrderStatusRequest Request { get; set; } = null!;
    public string? UpdatedBy { get; set; }
}

public class GetOrderStatsQuery : IRequest<ApiResponse<OrderStatsDto>>
{
    public OrderSearchRequest SearchRequest { get; set; } = null!;
}

public class OrderStatsDto
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }
}
