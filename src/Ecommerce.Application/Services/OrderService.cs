using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Common;
using Ecommerce.Domain.Entities.Ordering;
using System.Linq.Expressions;

namespace Ecommerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository orderRepo,
        ICartRepository cartRepo,
        IProductRepository productRepo,
        IUnitOfWork unitOfWork,
        IEventBus eventBus,
        ILogger<OrderService> logger)
    {
        _orderRepo = orderRepo;
        _cartRepo = cartRepo;
        _productRepo = productRepo;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<ApiResponse<OrderDto>> PlaceOrderAsync(Guid userId, PlaceOrderRequest request, CancellationToken ct = default)
    {
        var cart = await _cartRepo.GetByUserIdAsync(userId, ct);
        if (cart is null || !cart.Items.Any())
            return ApiResponse<OrderDto>.FailResponse("Cart is empty.", 400);

        var subtotal = cart.Items.Sum(i => i.Price * i.Quantity);
        var tax = Math.Round(subtotal * 0.08m, 2);
        var shippingCost = subtotal >= 50 ? 0 : 9.99m;
        var total = subtotal + tax + shippingCost - (request.DiscountAmount ?? 0);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            UserId = userId,
            SubTotal = subtotal,
            TaxAmount = tax,
            ShippingCost = shippingCost,
            DiscountAmount = request.DiscountAmount ?? 0,
            TotalAmount = total,
            Status = OrderStatus.Pending,
            PaymentStatus = PaymentStatus.Pending,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in cart.Items)
        {
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                TotalPrice = item.Price * item.Quantity
            });
        }

        await _orderRepo.AddAsync(order, ct);
        await _cartRepo.ClearCartAsync(cart.Id, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Order placed: {OrderNumber} by user {UserId}", order.OrderNumber, userId);

        return ApiResponse<OrderDto>.SuccessResponse(MapToDto(order));
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid id, CancellationToken ct = default)
    {
        var order = await _orderRepo.GetByIdAsync(id, ct);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);
        return ApiResponse<OrderDto>.SuccessResponse(MapToDto(order));
    }

    public async Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, CancellationToken ct = default)
    {
        var order = await _orderRepo.GetByOrderNumberAsync(orderNumber, ct);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);
        return ApiResponse<OrderDto>.SuccessResponse(MapToDto(order));
    }

    public async Task<ApiResponse<List<OrderDto>>> GetCustomerOrdersAsync(Guid userId, int page, int pageSize, CancellationToken ct = default)
    {
        var orders = await _orderRepo.GetByUserIdAsync(userId, ct);
        var paged = orders.Skip((page - 1) * pageSize).Take(pageSize).Select(MapToDto).ToList();
        return ApiResponse<List<OrderDto>>.SuccessResponse(paged);
    }

    public async Task<ApiResponse<PagedResponse<OrderDto>>> GetOrdersAsync(OrderSearchRequest request, CancellationToken ct = default)
    {
        Expression<Func<Order, bool>> predicate = o =>
            (!request.UserId.HasValue || o.UserId == request.UserId) &&
            (!request.Status.HasValue || o.Status == request.Status) &&
            (!request.FromDate.HasValue || o.CreatedAt >= request.FromDate) &&
            (!request.ToDate.HasValue || o.CreatedAt <= request.ToDate);

        var orders = await _orderRepo.FindAsync(predicate, ct);
        var totalCount = orders.Count();
        var paged = orders.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).Select(MapToDto).ToList();

        var response = new PagedResponse<OrderDto>
        {
            Data = paged,
            PageNumber = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            TotalRecords = totalCount
        };
        return ApiResponse<PagedResponse<OrderDto>>.SuccessResponse(response);
    }

    public async Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request, string? updatedBy, CancellationToken ct = default)
    {
        var order = await _orderRepo.GetByIdAsync(id, ct);
        if (order is null)
            return ApiResponse<OrderDto>.FailResponse("Order not found.", 404);

        order.Status = request.Status;
        order.UpdatedAt = DateTime.UtcNow;

        order.StatusHistory.Add(new OrderHistory
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Status = request.Status.ToString(),
            Notes = request.Notes,
            CreatedBy = updatedBy,
            CreatedAt = DateTime.UtcNow
        });

        await _orderRepo.UpdateAsync(order, ct);
        _logger.LogInformation("Order {OrderNumber} status updated to {Status}", order.OrderNumber, request.Status);

        return ApiResponse<OrderDto>.SuccessResponse(MapToDto(order));
    }

    public async Task<ApiResponse> CancelOrderAsync(Guid id, string? reason, string? cancelledBy, CancellationToken ct = default)
    {
        var order = await _orderRepo.GetByIdAsync(id, ct);
        if (order is null)
            return ApiResponse.FailResponse("Order not found.", 404);

        if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
            return ApiResponse.FailResponse("Cannot cancel order in current status.", 400);

        order.Status = OrderStatus.Cancelled;
        order.Notes = reason;
        order.UpdatedAt = DateTime.UtcNow;

        order.StatusHistory.Add(new OrderHistory
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Status = OrderStatus.Cancelled.ToString(),
            Notes = reason,
            CreatedBy = cancelledBy,
            CreatedAt = DateTime.UtcNow
        });

        await _orderRepo.UpdateAsync(order, ct);
        _logger.LogInformation("Order {OrderNumber} cancelled", order.OrderNumber);

        return ApiResponse.SuccessResponse("Order cancelled successfully.");
    }

    public async Task<ApiResponse<decimal>> CalculateTaxAsync(Guid userId, CancellationToken ct = default)
    {
        var cart = await _cartRepo.GetByUserIdAsync(userId, ct);
        if (cart is null) return ApiResponse<decimal>.SuccessResponse(0);
        var subtotal = cart.Items.Sum(i => i.Price * i.Quantity);
        return ApiResponse<decimal>.SuccessResponse(Math.Round(subtotal * 0.08m, 2));
    }

    private static OrderDto MapToDto(Order o) => new()
    {
        Id = o.Id,
        OrderNumber = o.OrderNumber,
        Status = o.Status.ToString(),
        SubTotal = o.SubTotal,
        TaxAmount = o.TaxAmount,
        ShippingCost = o.ShippingCost,
        DiscountAmount = o.DiscountAmount,
        TotalAmount = o.TotalAmount,
        ItemCount = o.Items.Count,
        CreatedAt = o.CreatedAt,
        Items = o.Items.Select(i => new OrderItemDto
        {
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            TotalPrice = i.TotalPrice
        }).ToList()
    };
}
