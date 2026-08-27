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

public class GetCartHandler : IRequestHandler<GetCartQuery, ApiResponse<CartDto>>
{
    private readonly ICartService _cartService;
    public GetCartHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse<CartDto>> Handle(GetCartQuery request, CancellationToken ct)
    {
        return await _cartService.GetCartAsync(request.UserId, request.SessionId, ct);
    }
}

public class AddToCartHandler : IRequestHandler<AddToCartCommand, ApiResponse<CartDto>>
{
    private readonly ICartService _cartService;
    public AddToCartHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse<CartDto>> Handle(AddToCartCommand request, CancellationToken ct)
    {
        return await _cartService.AddItemAsync(request.UserId, request.SessionId, request.Request, ct);
    }
}

public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, ApiResponse<CartDto>>
{
    private readonly ICartService _cartService;
    public UpdateCartItemHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse<CartDto>> Handle(UpdateCartItemCommand request, CancellationToken ct)
    {
        return await _cartService.UpdateItemAsync(request.UserId, request.SessionId, request.Request, ct);
    }
}

public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand, ApiResponse<CartDto>>
{
    private readonly ICartService _cartService;
    public RemoveCartItemHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse<CartDto>> Handle(RemoveCartItemCommand request, CancellationToken ct)
    {
        return await _cartService.RemoveItemAsync(request.UserId, request.SessionId, request.CartItemId, ct);
    }
}

public class ClearCartHandler : IRequestHandler<ClearCartCommand, ApiResponse>
{
    private readonly ICartService _cartService;
    public ClearCartHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse> Handle(ClearCartCommand request, CancellationToken ct)
    {
        return await _cartService.ClearCartAsync(request.UserId, request.SessionId, ct);
    }
}

public class ApplyCouponHandler : IRequestHandler<ApplyCouponCommand, ApiResponse<ApplyCouponResponse>>
{
    private readonly ICartService _cartService;
    public ApplyCouponHandler(ICartService cartService) => _cartService = cartService;
    public async Task<ApiResponse<ApplyCouponResponse>> Handle(ApplyCouponCommand request, CancellationToken ct)
    {
        return await _cartService.ApplyCouponAsync(request.UserId, request.SessionId, request.Request, ct);
    }
}

public class GetCartQuery : IRequest<ApiResponse<CartDto>>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
}

public class AddToCartCommand : IRequest<ApiResponse<CartDto>>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public AddToCartRequest Request { get; set; } = null!;
}

public class UpdateCartItemCommand : IRequest<ApiResponse<CartDto>>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public UpdateCartItemRequest Request { get; set; } = null!;
}

public class RemoveCartItemCommand : IRequest<ApiResponse<CartDto>>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public Guid CartItemId { get; set; }
}

public class ClearCartCommand : IRequest<ApiResponse>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
}

public class ApplyCouponCommand : IRequest<ApiResponse<ApplyCouponResponse>>
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public ApplyCouponRequest Request { get; set; } = null!;
}

public class ApplyCouponResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? CouponCode { get; set; }
}
