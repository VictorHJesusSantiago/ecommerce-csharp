using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.Application.DTOs.Report;

public class SalesReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalItemsSold { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal RefundAmount { get; set; }
    public int RefundCount { get; set; }
    public decimal NetRevenue => TotalRevenue - RefundAmount;
    public List<DailySalesDto> DailySales { get; set; } = [];
    public List<TopProductDto> TopProducts { get; set; } = [];
    public List<TopCategoryDto> TopCategories { get; set; } = [];
    public List<PaymentMethodSummaryDto> PaymentMethodSummary { get; set; } = [];
    public List<TopSellingProductDto> TopSellingProducts { get; set; } = [];
}

public class DailySalesDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
    public int ItemCount { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal RefundAmount { get; set; }
    public int NewCustomers { get; set; }
    public int ReturningCustomers { get; set; }
}

public class TopProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public decimal ProfitMargin { get; set; }
    public string? CategoryName { get; set; }
}

public class TopSellingProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Profit => Revenue - (UnitCost * QuantitySold);
    public decimal ProfitMargin => Revenue > 0 ? Math.Round((Profit / Revenue) * 100, 2) : 0;
}

public class TopCategoryDto
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
    public int ProductCount { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal GrowthPercentage { get; set; }
}

public class PaymentMethodSummaryDto
{
    public string PaymentMethod { get; set; } = string.Empty;
    public int TransactionCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Percentage { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public decimal SuccessRate { get; set; }
}

public class DashboardSummaryDto
{
    public decimal TodayRevenue { get; set; }
    public int TodayOrders { get; set; }
    public int TodayVisitors { get; set; }
    public decimal TodayConversionRate { get; set; }
    public decimal WeekRevenue { get; set; }
    public int WeekOrders { get; set; }
    public decimal MonthRevenue { get; set; }
    public int MonthOrders { get; set; }
    public int TotalProducts { get; set; }
    public int ActiveProducts { get; set; }
    public int TotalCustomers { get; set; }
    public int NewCustomersToday { get; set; }
    public int PendingOrders { get; set; }
    public int LowStockProducts { get; set; }
    public int OutOfStockProducts { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal RevenueGrowthPercentage { get; set; }
    public decimal OrderGrowthPercentage { get; set; }
    public List<DailySalesDto> RecentSales { get; set; } = [];
    public List<TopSellingProductDto> TopSellingProducts { get; set; } = [];
    public List<TopCategoryDto> TopCategories { get; set; } = [];
    public List<PaymentMethodSummaryDto> PaymentMethods { get; set; } = [];
}

public class CustomerReportDto
{
    public int TotalCustomers { get; set; }
    public int ActiveCustomers { get; set; }
    public int NewCustomersThisMonth { get; set; }
    public int NewCustomersThisWeek { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal CustomerLifetimeValue { get; set; }
    public decimal CustomerRetentionRate { get; set; }
    public decimal CustomerChurnRate { get; set; }
    public int RepeatPurchaseRate { get; set; }
    public List<CustomerSegmentDto> Segments { get; set; } = [];
    public List<TopCustomerDto> TopCustomers { get; set; } = [];
    public List<GeographicDistributionDto> GeographicDistribution { get; set; } = [];
}

public class CustomerSegmentDto
{
    public string SegmentName { get; set; } = string.Empty;
    public int CustomerCount { get; set; }
    public decimal Revenue { get; set; }
    public decimal Percentage { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int AverageOrdersPerCustomer { get; set; }
}

public class TopCustomerDto
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageOrderValue { get; set; }
    public DateTime LastOrderDate { get; set; }
    public DateTime MemberSince { get; set; }
    public string LoyaltyTier { get; set; } = string.Empty;
}

public class GeographicDistributionDto
{
    public string Country { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? City { get; set; }
    public int CustomerCount { get; set; }
    public decimal Revenue { get; set; }
    public decimal Percentage { get; set; }
    public int OrderCount { get; set; }
    public decimal AverageOrderValue { get; set; }
}

public class ProductPerformanceDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public int TotalViews { get; set; }
    public int UniqueViews { get; set; }
    public int TotalSales { get; set; }
    public decimal Revenue { get; set; }
    public decimal ConversionRate { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int AddToCartCount { get; set; }
    public int WishlistCount { get; set; }
    public decimal ReturnRate { get; set; }
    public decimal AverageTimeToPurchase { get; set; }
    public List<DailySalesDto> SalesHistory { get; set; } = [];
}

public class InventoryReportDto
{
    public int TotalProducts { get; set; }
    public int TotalSKUs { get; set; }
    public int TotalStockQuantity { get; set; }
    public decimal TotalInventoryValue { get; set; }
    public int LowStockProducts { get; set; }
    public int OutOfStockProducts { get; set; }
    public int OverStockProducts { get; set; }
    public decimal AverageStockLevel { get; set; }
    public decimal StockTurnoverRate { get; set; }
    public int DaysOfSupply { get; set; }
    public List<WarehouseInventorySummaryDto> WarehouseSummaries { get; set; } = [];
    public List<LowStockProductDto> LowStockProductsList { get; set; } = [];
    public List<DeadStockProductDto> DeadStockProducts { get; set; } = [];
}

public class WarehouseInventorySummaryDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    public int TotalQuantity { get; set; }
    public int LowStockProducts { get; set; }
    public decimal TotalValue { get; set; }
    public decimal CapacityUtilization { get; set; }
    public int InboundShipments { get; set; }
    public int OutboundShipments { get; set; }
}

public class LowStockProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? CategoryName { get; set; }
    public int CurrentStock { get; set; }
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public int? LeadTimeDays { get; set; }
    public string? SupplierName { get; set; }
    public decimal UnitCost { get; set; }
    public decimal ReorderCost => UnitCost * ReorderQuantity;
    public bool IsReorderUrgent { get; set; }
}

public class DeadStockProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int CurrentStock { get; set; }
    public int DaysSinceLastSale { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalValue => UnitCost * CurrentStock;
    public DateTime? LastSoldDate { get; set; }
    public int SuggestedAction { get; set; }
}

public class ExportRequest
{
    public string ReportType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Format { get; set; } = "CSV";
    public List<string> Columns { get; set; } = [];
    public Dictionary<string, string> Filters { get; set; } = [];
    public string? GroupBy { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
}
