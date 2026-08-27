namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProductDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResponse<ProductListDto>>> SearchProductsAsync(ProductSearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProductDto>> UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponse> PublishProductAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProductDto>>> GetFeaturedProductsAsync(int count = 10, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProductDto>>> GetNewArrivalsAsync(int count = 10, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProductDto>>> GetBestSellersAsync(int count = 10, CancellationToken cancellationToken = default);
    Task<ApiResponse<int>> GetStockQuantityAsync(Guid productId, CancellationToken cancellationToken = default);
}

public interface ICategoryService
{
    Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ICartService
{
    Task<ApiResponse<CartDto>> GetCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> AddToCartAsync(Guid? userId, string? sessionId, AddToCartRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> RemoveFromCartAsync(Guid? userId, string? sessionId, Guid productId, string? variantId, CancellationToken cancellationToken = default);
    Task<ApiResponse> UpdateCartItemAsync(Guid? userId, string? sessionId, Guid productId, UpdateCartItemRequest request, string? variantId = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> ClearCartAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default);
}

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponse<OrderDto>> GetOrderByNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResponse<OrderDto>>> GetOrdersAsync(OrderSearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request, string? performedBy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse> CancelOrderAsync(Guid id, string reason, string? cancelledBy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<OrderDto>>> GetCustomerOrdersAsync(Guid customerId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}

public interface IReviewService
{
    Task<ApiResponse<List<ReviewDto>>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ReviewDto>> CreateReviewAsync(Guid userId, CreateReviewRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<ReviewDto>> ApproveReviewAsync(Guid reviewId, CancellationToken cancellationToken = default);
    Task<ApiResponse> VoteReviewAsync(Guid reviewId, Guid userId, bool isHelpful, CancellationToken cancellationToken = default);
}

public interface ICouponService
{
    Task<ApiResponse<CouponDto>> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<ApiResponse<CouponDto>> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<ValidateCouponResponse>> ValidateCouponAsync(ValidateCouponRequest request, Guid? customerId = null, CancellationToken cancellationToken = default);
    Task<ApiResponse> DeactivateCouponAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IBannerService
{
    Task<ApiResponse<List<BannerDto>>> GetActiveBannersAsync(string position, CancellationToken cancellationToken = default);
    Task<ApiResponse<BannerDto>> CreateBannerAsync(CreateBannerRequest request, CancellationToken cancellationToken = default);
}

public interface INotificationService
{
    Task<ApiResponse<List<NotificationDto>>> GetUserNotificationsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ApiResponse> SendNotificationAsync(SendNotificationRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default);
    Task<ApiResponse<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
}

public interface IDashboardService
{
    Task<ApiResponse<DashboardDto>> GetDashboardAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesSummaryDto>> GetSalesSummaryAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
}

public interface IInventoryService
{
    Task<ApiResponse<List<WarehouseDto>>> GetWarehousesAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<WarehouseDto>> CreateWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<InventoryDashboardDto>> GetInventoryDashboardAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse> AdjustStockAsync(AdjustStockRequest request, string? performedBy = null, CancellationToken cancellationToken = default);
    Task<ApiResponse> TransferStockAsync(TransferStockRequest request, string? performedBy = null, CancellationToken cancellationToken = default);
}

public interface IReportService
{
    Task<ApiResponse<SalesSummaryDto>> GetSalesReportAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<ApiResponse<CustomerAnalyticsDto>> GetCustomerAnalyticsAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<ApiResponse<InventoryReportDto>> GetInventoryReportAsync(CancellationToken cancellationToken = default);
}

public interface ICmsService
{
    Task<ApiResponse<CmsPageDto>> GetPageBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<ApiResponse<CmsPageDto>> CreatePageAsync(CreateCmsPageRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CmsPageDto>> UpdatePageAsync(Guid id, UpdateCmsPageRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse> PublishPageAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface INavigationService
{
    Task<ApiResponse<List<NavigationMenuDto>>> GetMenusByLocationAsync(string location, CancellationToken cancellationToken = default);
    Task<ApiResponse<NavigationMenuDto>> CreateMenuAsync(CreateNavigationMenuRequest request, CancellationToken cancellationToken = default);
}

using Wrappers;
using DTOs.Product;
using DTOs.Order;
using DTOs.Cart;
using DTOs.User;
using DTOs.Review;
using DTOs.Marketing;
using DTOs.Notification;
using DTOs.Report;
using DTOs.Inventory;
using DTOs.CMS;
