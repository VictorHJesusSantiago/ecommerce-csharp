using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Application.DTOs.Catalog;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsDigital { get; set; }
    public bool RequiresShipping { get; set; } = true;
    public double Weight { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public string? MainImageUrl { get; set; }
    public List<ProductImageDto> Images { get; set; } = [];
    public List<ProductVariantDto> Variants { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int TotalSales { get; set; }
    public bool InStock => StockQuantity > 0;
    public int ViewCount { get; set; }
    public decimal ProfitMargin => Price > 0 && CostPrice.HasValue ? Math.Round(((Price - CostPrice.Value) / Price) * 100, 2) : 0;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string? Sku { get; set; }
    public string? MainImageUrl { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public bool InStock { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsOnSale => CompareAtPrice.HasValue && CompareAtPrice > Price;
    public decimal? DiscountPercentage => CompareAtPrice.HasValue && CompareAtPrice > Price
        ? Math.Round((1 - Price / CompareAtPrice.Value) * 100, 2)
        : null;
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int TotalSales { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductImageDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsPrimary { get; set; }
}

public class ProductVariantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = [];
    public string? ImageUrl { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    public bool IsDigital { get; set; }
    public bool RequiresShipping { get; set; } = true;
    public double Weight { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public string? MainImageUrl { get; set; }
    public List<CreateProductImageRequest> Images { get; set; } = [];
    public List<CreateProductVariantRequest> Variants { get; set; } = [];
    public List<string> Tags { get; set; } = [];
}

public class UpdateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public decimal? Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public int? StockQuantity { get; set; }
    public int? LowStockThreshold { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsFeatured { get; set; }
    public bool? IsDigital { get; set; }
    public bool? RequiresShipping { get; set; }
    public double? Weight { get; set; }
    public double? Length { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public string? MainImageUrl { get; set; }
    public List<string> Tags { get; set; } = [];
}

public class CreateProductImageRequest
{
    public string Url { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public int DisplayOrder { get; set; }
}

public class CreateProductVariantRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int StockQuantity { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = [];
    public string? ImageUrl { get; set; }
}

public class ProductSearchRequest
{
    public string? SearchQuery { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? InStockOnly { get; set; }
    public bool? IsFeatured { get; set; }
    public bool? IsOnSale { get; set; }
    public double? MinRating { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public List<string> Tags { get; set; } = [];
}

public class ProductStockDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int TotalStockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => TotalStockQuantity - ReservedQuantity;
    public int LowStockThreshold { get; set; }
    public bool IsLowStock => AvailableQuantity <= LowStockThreshold;
    public bool IsOutOfStock => AvailableQuantity <= 0;
    public List<WarehouseStockDto> WarehouseStocks { get; set; } = [];
}

public class WarehouseStockDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
}

public class BrandDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public string? Slug { get; set; }
    public bool IsActive { get; set; } = true;
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBrandRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
}

public class UpdateBrandRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public bool? IsActive { get; set; }
}
