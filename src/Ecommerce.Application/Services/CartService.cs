using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;
    private readonly ILogger<CartService> _logger;

    public CartService(ICartRepository cartRepo, IProductRepository productRepo, ILogger<CartService> logger)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
        _logger = logger;
    }

    public async Task<ApiResponse<CartDto>> GetCartAsync(Guid? userId, string? sessionId, CancellationToken ct = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, ct);
        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> AddToCartAsync(Guid? userId, string? sessionId, AddToCartRequest request, CancellationToken ct = default)
    {
        var product = await _productRepo.GetByIdAsync(request.ProductId, ct);
        if (product is null)
            return ApiResponse<CartDto>.FailResponse("Product not found.", 404);

        if (product.StockQuantity < request.Quantity)
            return ApiResponse<CartDto>.FailResponse("Insufficient stock.", 400);

        var cart = await GetOrCreateCartAsync(userId, sessionId, ct);
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId && i.VariantId == request.VariantId);

        if (existingItem is not null)
        {
            existingItem.Quantity += request.Quantity;
            existingItem.TotalPrice = existingItem.Price * existingItem.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = request.ProductId,
                VariantId = request.VariantId,
                ProductName = product.Name,
                Sku = product.Sku,
                Price = product.Price,
                Quantity = request.Quantity,
                TotalPrice = product.Price * request.Quantity,
                ImageUrl = product.ImageUrl
            });
        }

        await _cartRepo.UpdateAsync(cart, ct);
        _logger.LogInformation("Item added to cart: {ProductName} x{Quantity}", product.Name, request.Quantity);

        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> UpdateCartItemAsync(Guid? userId, string? sessionId, Guid productId, UpdateCartItemRequest request, string? variantId = null, CancellationToken cancellationToken = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, cancellationToken);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.VariantId == variantId);
        if (item is null)
            return ApiResponse<CartDto>.FailResponse("Item not found in cart.", 404);

        item.Quantity = request.Quantity;
        item.TotalPrice = item.Price * item.Quantity;
        await _cartRepo.UpdateAsync(cart, cancellationToken);

        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> RemoveFromCartAsync(Guid? userId, string? sessionId, Guid productId, string? variantId = null, CancellationToken cancellationToken = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, cancellationToken);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.VariantId == variantId);
        if (item is null)
            return ApiResponse<CartDto>.FailResponse("Item not found in cart.", 404);

        cart.Items.Remove(item);
        await _cartRepo.UpdateAsync(cart, cancellationToken);

        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> ClearCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, cancellationToken);
        cart.Items.Clear();
        await _cartRepo.UpdateAsync(cart, cancellationToken);
        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<CartDto>> ApplyCouponAsync(Guid? userId, string? sessionId, string couponCode, CancellationToken cancellationToken = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, cancellationToken);
        cart.CouponCode = couponCode;
        await _cartRepo.UpdateAsync(cart, cancellationToken);
        return ApiResponse<CartDto>.SuccessResponse(MapToDto(cart));
    }

    public async Task<ApiResponse<int>> GetCartItemCountAsync(Guid? userId, string? sessionId, CancellationToken ct = default)
    {
        var cart = await GetOrCreateCartAsync(userId, sessionId, ct);
        return ApiResponse<int>.SuccessResponse(cart.Items.Sum(i => i.Quantity));
    }

    private async Task<ShoppingCart> GetOrCreateCartAsync(Guid? userId, string? sessionId, CancellationToken ct)
    {
        ShoppingCart? cart = null;
        if (userId.HasValue)
            cart = await _cartRepo.GetByUserIdAsync(userId.Value, ct);
        else if (!string.IsNullOrEmpty(sessionId))
            cart = await _cartRepo.GetBySessionIdAsync(sessionId, ct);

        if (cart is null)
        {
            cart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SessionId = sessionId,
                CreatedAt = DateTime.UtcNow
            };
            await _cartRepo.AddAsync(cart, ct);
        }
        return cart;
    }

    private static CartDto MapToDto(ShoppingCart cart) => new()
    {
        Id = cart.Id,
        Items = cart.Items.Select(i => new CartItemDto
        {
            ProductId = i.ProductId,
            ProductName = i.ProductName,
            Sku = i.Sku,
            Price = i.Price,
            Quantity = i.Quantity,
            Total = i.TotalPrice,
            ImageUrl = i.ImageUrl
        }).ToList(),
        SubTotal = cart.Items.Sum(i => i.TotalPrice),
        Total = cart.Items.Sum(i => i.TotalPrice),
        CouponCode = cart.CouponCode
    };
}
