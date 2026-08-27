using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models;

public class HomeIndexViewModel
{
    public List<ProductListDto> FeaturedProducts { get; set; } = [];
    public List<ProductListDto> NewArrivals { get; set; } = [];
    public List<ProductListDto> SaleProducts { get; set; } = [];
    public List<CategoryDto> Categories { get; set; } = [];
    public List<BannerDto> Banners { get; set; } = [];
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
}

public class ProductListViewModel
{
    public List<ProductListDto> Products { get; set; } = [];
    public List<CategoryDto> Categories { get; set; } = [];
    public List<BrandDto> Brands { get; set; } = [];
    public string? SearchQuery { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? InStockOnly { get; set; }
    public bool? IsOnSale { get; set; }
    public double? MinRating { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

public class ProductDetailViewModel
{
    public ProductDto? Product { get; set; }
    public List<ReviewDto> Reviews { get; set; } = [];
    public ReviewStatsDto? ReviewStats { get; set; }
    public List<ProductListDto> RelatedProducts { get; set; } = [];
    public bool IsInWishlist { get; set; }
}

public class CategoryListViewModel
{
    public List<CategoryDto> Categories { get; set; } = [];
}

public class CategoryDetailViewModel
{
    public CategoryDto? Category { get; set; }
    public List<ProductListDto> Products { get; set; } = [];
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
}

public class CartViewModel
{
    public List<CartItemDto> Items { get; set; } = [];
    public string? CouponCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public int ItemCount => Items.Sum(i => i.Quantity);
}

public class CheckoutViewModel
{
    public List<CartItemDto> Items { get; set; } = [];
    public List<UserAddressDto> SavedAddresses { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CouponCode { get; set; }
}

public class OrderHistoryViewModel
{
    public List<OrderDto> Orders { get; set; } = [];
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
}

public class OrderDetailViewModel
{
    public OrderDto? Order { get; set; }
    public ShippingTrackingResultDto? TrackingInfo { get; set; }
}

public class AccountIndexViewModel
{
    public UserDto? User { get; set; }
    public int RecentOrderCount { get; set; }
    public int WishlistItemCount { get; set; }
    public int SavedAddressCount { get; set; }
    public List<OrderDto> RecentOrders { get; set; } = [];
}

public class AccountSettingsViewModel
{
    public UserDto? User { get; set; }
}

public class AccountAddressesViewModel
{
    public List<UserAddressDto> Addresses { get; set; } = [];
}

public class AccountPaymentMethodsViewModel
{
    public List<PaymentMethodDto> PaymentMethods { get; set; } = [];
}

public class AccountWishlistViewModel
{
    public List<WishlistItemDto> Items { get; set; } = [];
    public int ItemCount => Items.Count;
}

public class SearchViewModel
{
    public string? Query { get; set; }
    public SearchResultDto? Results { get; set; }
    public List<string> RecentSearches { get; set; } = [];
    public List<PopularSearchDto> PopularSearches { get; set; } = [];
}

public class AboutViewModel
{
    public string Title { get; set; } = "About Us";
    public string Content { get; set; } = string.Empty;
    public List<TeamMemberViewModel> Team { get; set; } = [];
}

public class TeamMemberViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}

public class ContactViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;
}

public class AdminDashboardViewModel
{
    public DashboardDto? Dashboard { get; set; }
    public List<OrderDto> RecentOrders { get; set; } = [];
    public InventorySummaryDto? InventorySummary { get; set; }
    public MarketingSummaryDto? MarketingSummary { get; set; }
}

public class AdminProductsViewModel
{
    public List<ProductListDto> Products { get; set; } = [];
    public string? SearchQuery { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public bool? IsActive { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalCount { get; set; }
}

public class AdminOrdersViewModel
{
    public List<OrderDto> Orders { get; set; } = [];
    public string? SearchQuery { get; set; }
    public string? Status { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalCount { get; set; }
}

public class AdminCustomersViewModel
{
    public List<UserDto> Customers { get; set; } = [];
    public string? SearchQuery { get; set; }
    public bool? IsActive { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalCount { get; set; }
}

public class AdminOrderDetailViewModel
{
    public OrderDto? Order { get; set; }
    public List<OrderHistoryDto> History { get; set; } = [];
    public ShippingTrackingResultDto? TrackingInfo { get; set; }
}

public class AdminEditProductViewModel
{
    public ProductDto? Product { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
    public List<BrandDto> Brands { get; set; } = [];
}

public class AdminReportsViewModel
{
    public ReportSummaryDto? Report { get; set; }
    public string Period { get; set; } = "month";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class AdminRevenueReportViewModel
{
    public RevenueReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminSalesReportViewModel
{
    public SalesReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminCustomerReportViewModel
{
    public CustomerReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminInventoryReportViewModel
{
    public InventoryReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminPaymentReportViewModel
{
    public PaymentReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminMarketingReportViewModel
{
    public MarketingReportDto? Report { get; set; }
    public string Period { get; set; } = "month";
}

public class AdminProductPerformanceViewModel
{
    public List<ProductPerformanceDto> Products { get; set; } = [];
    public string Period { get; set; } = "month";
}

public class AdminNotificationViewModel
{
    public List<NotificationBatchDto> Notifications { get; set; } = [];
    public NotificationAnalyticsDto? Analytics { get; set; }
}

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public string? ErrorMessage { get; set; }
}
