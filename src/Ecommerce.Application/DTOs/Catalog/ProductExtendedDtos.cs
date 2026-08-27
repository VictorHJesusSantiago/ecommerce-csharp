using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Application.DTOs.Catalog;

public class ProductCollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? BannerColor { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public string? FilterCriteria { get; set; }
    public int ProductCount { get; set; }
    public List<ProductListDto> Products { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateCollectionRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? BannerColor { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    public List<Guid> ProductIds { get; set; } = [];
}

public class UpdateCollectionRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? BannerColor { get; set; }
    public int? DisplayOrder { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsFeatured { get; set; }
}

public class ProductTagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTagRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}

public class ProductComparisonDto
{
    public List<ProductDto> Products { get; set; } = [];
    public List<string> ComparisonAttributes { get; set; } = [];
    public Dictionary<string, Dictionary<Guid, string>> AttributeValues { get; set; } = [];
}

public class ProductRecommendationDto
{
    public string Type { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public List<ProductListDto> Products { get; set; } = [];
    public int MaxItems { get; set; } = 10;
}

public class ProductBulkOperationDto
{
    public string Operation { get; set; } = string.Empty;
    public List<Guid> ProductIds { get; set; } = [];
    public Dictionary<string, object> Parameters { get; set; } = [];
    public string? initiatedBy { get; set; }
    public DateTime initiatedAt { get; set; }
    public string Status { get; set; } = "Pending";
    public int ProcessedCount { get; set; }
    public int FailedCount { get; set; }
    public List<string> Errors { get; set; } = [];
}

public class StockMovementDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public Guid? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public int Quantity { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public string? PerformedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductAuditDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? PerformedBy { get; set; }
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ProductAnalyticsDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int TotalViews { get; set; }
    public int UniqueViews { get; set; }
    public int TotalSales { get; set; }
    public decimal Revenue { get; set; }
    public double ConversionRate { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int AddToCartCount { get; set; }
    public int WishlistCount { get; set; }
    public int ShareCount { get; set; }
    public decimal ReturnRate { get; set; }
    public List<DailyViewDto> DailyViews { get; set; } = [];
    public List<DailySalesDto> DailySales { get; set; } = [];
}

public class DailyViewDto
{
    public DateTime Date { get; set; }
    public int Views { get; set; }
    public int UniqueViews { get; set; }
    public int AddToCartCount { get; set; }
    public int PurchaseCount { get; set; }
}

public class DailySalesDto
{
    public DateTime Date { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public decimal AveragePrice { get; set; }
    public int OrderCount { get; set; }
    public int UniqueCustomers { get; set; }
}

public class ProductSeoDto
{
    public Guid ProductId { get; set; }
    public string MetaTitle { get; set; } = string.Empty;
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? SchemaMarkup { get; set; }
    public string? AltText { get; set; }
    public DateTime? LastModified { get; set; }
}

public class ProductPriceHistoryDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public string? Reason { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}

public class ProductInventoryAlertDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int CurrentStock { get; set; }
    public int LowStockThreshold { get; set; }
    public bool IsOutOfStock { get; set; }
    public bool IsLowStock { get; set; }
    public int DaysSinceLastRestock { get; set; }
    public decimal AverageDailySales { get; set; }
    public int DaysUntilStockout { get; set; }
    public string? SupplierName { get; set; }
    public int LeadTimeDays { get; set; }
    public bool RequiresReorder { get; set; }
    public int RecommendedOrderQuantity { get; set; }
}
