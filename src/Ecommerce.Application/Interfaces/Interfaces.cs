using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<ProductDto>> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<ApiResponse<PagedResponse<ProductListDto>>> SearchProductsAsync(ProductSearchRequest request, CancellationToken ct = default);
    Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductRequest request, CancellationToken ct = default);
    Task<ApiResponse<ProductDto>> UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken ct = default);
    Task<ApiResponse> DeleteProductAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse> PublishProductAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<List<ProductDto>>> GetFeaturedProductsAsync(int count = 10, CancellationToken ct = default);
    Task<ApiResponse<List<ProductDto>>> GetNewArrivalsAsync(int count = 10, CancellationToken ct = default);
    Task<ApiResponse<List<ProductDto>>> GetBestSellersAsync(int count = 10, CancellationToken ct = default);
    Task<ApiResponse<PagedResponse<ProductListDto>>> GetByCategoryAsync(Guid categoryId, int page, int pageSize, CancellationToken ct = default);
    Task<ApiResponse<PagedResponse<ProductListDto>>> GetByBrandAsync(Guid brandId, int page, int pageSize, CancellationToken ct = default);
}

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync(CancellationToken ct = default);
    Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<CategoryDto>> GetCategoryBySlugAsync(string slug, CancellationToken ct = default);
    Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct = default);
    Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default);
    Task<ApiResponse> DeleteCategoryAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<int>> GetCategoryProductCountAsync(Guid categoryId, CancellationToken ct = default);
}

public interface ICartService
{
    Task<ApiResponse<CartDto>> GetCartAsync(Guid? userId, string? sessionId, CancellationToken ct = default);
    Task<ApiResponse<CartDto>> AddToCartAsync(Guid? userId, string? sessionId, AddToCartRequest request, CancellationToken ct = default);
    Task<ApiResponse<CartDto>> UpdateCartItemAsync(Guid? userId, string? sessionId, Guid productId, UpdateCartItemRequest request, string? variantId = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> RemoveFromCartAsync(Guid? userId, string? sessionId, Guid productId, string? variantId = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> ClearCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> ApplyCouponAsync(Guid? userId, string? sessionId, string couponCode, CancellationToken cancellationToken = default);
    Task<ApiResponse<int>> GetCartItemCountAsync(Guid? userId, string? sessionId, CancellationToken ct = default);
}

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> PlaceOrderAsync(Guid userId, PlaceOrderRequest request, CancellationToken ct = default);
    Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, CancellationToken ct = default);
    Task<ApiResponse<List<OrderDto>>> GetCustomerOrdersAsync(Guid userId, int page, int pageSize, CancellationToken ct = default);
    Task<ApiResponse<PagedResponse<OrderDto>>> GetOrdersAsync(OrderSearchRequest request, CancellationToken ct = default);
    Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request, string? updatedBy, CancellationToken ct = default);
    Task<ApiResponse> CancelOrderAsync(Guid id, string? reason, string? cancelledBy, CancellationToken ct = default);
    Task<ApiResponse<decimal>> CalculateTaxAsync(Guid userId, CancellationToken ct = default);
}

public interface IReviewService
{
    Task<ApiResponse<List<ReviewDto>>> GetProductReviewsAsync(Guid productId, CancellationToken ct = default);
    Task<ApiResponse<ReviewDto>> GetReviewByIdAsync(Guid reviewId, CancellationToken ct = default);
    Task<ApiResponse<ReviewDto>> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken ct = default);
    Task<ApiResponse<ReviewDto>> UpdateReviewAsync(Guid reviewId, Guid userId, UpdateReviewRequest request, CancellationToken ct = default);
    Task<ApiResponse> DeleteReviewAsync(Guid reviewId, Guid userId, CancellationToken ct = default);
    Task<ApiResponse> ApproveReviewAsync(Guid reviewId, CancellationToken ct = default);
    Task<ApiResponse> VoteReviewAsync(Guid reviewId, Guid userId, bool isHelpful, CancellationToken ct = default);
    Task<ApiResponse<ReviewStatsDto>> GetReviewStatsAsync(Guid productId, CancellationToken ct = default);
}

public interface ICouponService
{
    Task<ApiResponse<ValidateCouponResponse>> ValidateCouponAsync(string code, decimal orderTotal, CancellationToken ct = default);
    Task<ApiResponse<CouponDto>> CreateCouponAsync(CreateCouponRequest request, CancellationToken ct = default);
    Task<ApiResponse<CouponDto>> UpdateCouponAsync(Guid id, UpdateCouponRequest request, CancellationToken ct = default);
    Task<ApiResponse> DeleteCouponAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponse<List<CouponDto>>> GetActiveCouponsAsync(CancellationToken ct = default);
}

public interface IInventoryService
{
    Task<ApiResponse<List<InventoryItemDto>>> GetInventoryAsync(Guid? warehouseId = null, CancellationToken ct = default);
    Task<ApiResponse<List<InventoryItemDto>>> GetLowStockProductsAsync(int threshold = 10, CancellationToken ct = default);
    Task<ApiResponse> AdjustStockAsync(AdjustStockRequest request, CancellationToken ct = default);
    Task<ApiResponse> TransferStockAsync(TransferStockRequest request, CancellationToken ct = default);
    Task<ApiResponse<WarehouseDto>> CreateWarehouseAsync(CreateWarehouseRequest request, CancellationToken ct = default);
    Task<ApiResponse<SupplierDto>> CreateSupplierAsync(CreateSupplierRequest request, CancellationToken ct = default);
}

public interface INotificationService
{
    Task SendOrderConfirmationAsync(Guid orderId, CancellationToken ct = default);
    Task SendOrderStatusUpdateAsync(Guid orderId, string status, CancellationToken ct = default);
    Task SendWelcomeEmailAsync(Guid userId, CancellationToken ct = default);
    Task SendPasswordResetAsync(string email, string resetToken, CancellationToken ct = default);
    Task SendLowStockAlertAsync(Guid productId, int currentStock, CancellationToken ct = default);
    Task SendShippingConfirmationAsync(Guid orderId, string trackingNumber, CancellationToken ct = default);
}

public interface IExportService
{
    byte[] ExportOrdersToCsv(List<OrderDto> orders);
    byte[] ExportProductsToCsv(List<ProductListDto> products);
    string ExportToJson(object data);
}
