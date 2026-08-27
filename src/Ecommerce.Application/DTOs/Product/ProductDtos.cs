using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.DTOs.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public bool IsInStock => StockQuantity > 0;
    public bool IsOnSale => CompareAtPrice.HasValue && CompareAtPrice > Price;
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public List<ProductImageDto> Images { get; set; } = [];
    public List<ProductVariantDto> Variants { get; set; } = [];
    public Dictionary<string, string> Attributes { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool InStock { get; set; }
    public bool IsOnSale => CompareAtPrice.HasValue && CompareAtPrice > Price;
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public string? CategoryName { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string Sku { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; } = 10;
    public int? Weight { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsDigital { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public List<ProductVariantDto> Variants { get; set; } = [];
    public List<string> Tags { get; set; } = [];
}

public class UpdateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public decimal? Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int? StockQuantity { get; set; }
    public int? LowStockThreshold { get; set; }
    public int? Weight { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public bool? IsFeatured { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProductImageDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public bool IsMain { get; set; }
}

public class ProductVariantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int StockQuantity { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = [];
}

public class ProductSearchRequest
{
    public string? SearchQuery { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public bool InStockOnly { get; set; }
}

public class ProductStockDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int TotalStock { get; set; }
    public int ReservedStock { get; set; }
    public int AvailableStock => TotalStock - ReservedStock;
    public bool IsLowStock { get; set; }
    public bool IsOutOfStock => TotalStock == 0;
    public List<WarehouseStockDto> WarehouseStocks { get; set; } = [];
}

public class WarehouseStockDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
}
