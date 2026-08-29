using Xunit;
using FluentAssertions;
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

namespace Ecommerce.UnitTests;

public class ProductDtoComprehensiveTests
{
    [Fact]
    public void ProductDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Description = "Description",
            ShortDescription = "Short",
            Slug = "test-product",
            Price = 49.99m,
            CompareAtPrice = 69.99m,
            CostPrice = 30m,
            Sku = "SKU-001",
            Barcode = "1234567890123",
            StockQuantity = 100,
            LowStockThreshold = 10,
            IsActive = true,
            IsFeatured = true,
            IsDigital = false,
            RequiresShipping = true,
            Weight = 2.5,
            Length = 10.0,
            Width = 5.0,
            Height = 3.0,
            CategoryId = Guid.NewGuid(),
            CategoryName = "Electronics",
            BrandId = Guid.NewGuid(),
            BrandName = "TechBrand",
            MainImageUrl = "https://example.com/image.jpg",
            AverageRating = 4.5,
            ReviewCount = 128,
            TotalSales = 500,
            ViewCount = 1000,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PublishedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Test Product");
        dto.Price.Should().Be(49.99m);
        dto.Sku.Should().Be("SKU-001");
        dto.StockQuantity.Should().Be(100);
        dto.IsActive.Should().BeTrue();
        dto.IsFeatured.Should().BeTrue();
    }

    [Fact]
    public void ProductDto_InStock_ShouldReturnTrueWhenStockPositive()
    {
        var dto = new ProductDto { StockQuantity = 10 };

        dto.InStock.Should().BeTrue();
    }

    [Fact]
    public void ProductDto_InStock_ShouldReturnFalseWhenStockZero()
    {
        var dto = new ProductDto { StockQuantity = 0 };

        dto.InStock.Should().BeFalse();
    }

    [Fact]
    public void ProductDto_IsOnSale_ShouldReturnTrueWhenCompareAtPriceHigher()
    {
        var dto = new ProductDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.IsOnSale.Should().BeTrue();
    }

    [Fact]
    public void ProductDto_IsOnSale_ShouldReturnFalseWhenNoCompareAtPrice()
    {
        var dto = new ProductDto { Price = 49.99m };

        dto.IsOnSale.Should().BeFalse();
    }

    [Fact]
    public void ProductDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.DiscountPercentage.Should().Be(28.57m);
    }

    [Fact]
    public void ProductDto_ProfitMargin_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto { Price = 100m, CostPrice = 60m };

        dto.ProfitMargin.Should().Be(40m);
    }

    [Fact]
    public void ProductDto_ProfitMargin_ShouldReturnZeroWhenNoCostPrice()
    {
        var dto = new ProductDto { Price = 100m };

        dto.ProfitMargin.Should().Be(0);
    }

    [Fact]
    public void ProductDto_Images_ShouldBeEmptyByDefault()
    {
        var dto = new ProductDto();

        dto.Images.Should().NotBeNull();
        dto.Images.Should().BeEmpty();
    }

    [Fact]
    public void ProductDto_Variants_ShouldBeEmptyByDefault()
    {
        var dto = new ProductDto();

        dto.Variants.Should().NotBeNull();
        dto.Variants.Should().BeEmpty();
    }

    [Fact]
    public void ProductDto_Tags_ShouldBeEmptyByDefault()
    {
        var dto = new ProductDto();

        dto.Tags.Should().NotBeNull();
        dto.Tags.Should().BeEmpty();
    }
}

public class ProductListDtoComprehensiveTests
{
    [Fact]
    public void ProductListDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductListDto
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            ShortDescription = "Short description",
            Slug = "test-product",
            Price = 49.99m,
            CompareAtPrice = 69.99m,
            Sku = "SKU-001",
            MainImageUrl = "https://example.com/image.jpg",
            CategoryId = Guid.NewGuid(),
            CategoryName = "Electronics",
            BrandId = Guid.NewGuid(),
            BrandName = "TechBrand",
            InStock = true,
            IsFeatured = true,
            AverageRating = 4.5,
            ReviewCount = 128,
            TotalSales = 500,
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Test Product");
        dto.Price.Should().Be(49.99m);
        dto.InStock.Should().BeTrue();
        dto.IsFeatured.Should().BeTrue();
    }

    [Fact]
    public void ProductListDto_IsOnSale_ShouldReturnTrueWhenCompareAtPriceHigher()
    {
        var dto = new ProductListDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.IsOnSale.Should().BeTrue();
    }

    [Fact]
    public void ProductListDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ProductListDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.DiscountPercentage.Should().Be(28.57m);
    }

    [Fact]
    public void ProductListDto_DiscountPercentage_ShouldReturnNullWhenNoCompareAtPrice()
    {
        var dto = new ProductListDto { Price = 49.99m };

        dto.DiscountPercentage.Should().BeNull();
    }
}

public class ProductSearchRequestComprehensiveTests
{
    [Fact]
    public void ProductSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new ProductSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.SortDescending.Should().BeTrue();
        request.SearchQuery.Should().BeNull();
        request.CategoryId.Should().BeNull();
        request.BrandId.Should().BeNull();
        request.MinPrice.Should().BeNull();
        request.MaxPrice.Should().BeNull();
        request.InStockOnly.Should().BeNull();
        request.IsFeatured.Should().BeNull();
        request.IsOnSale.Should().BeNull();
        request.MinRating.Should().BeNull();
        request.SortBy.Should().BeNull();
    }

    [Fact]
    public void ProductSearchRequest_WithFilters_ShouldSetFilters()
    {
        var request = new ProductSearchRequest
        {
            SearchQuery = "headphones",
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            MinPrice = 20m,
            MaxPrice = 100m,
            InStockOnly = true,
            IsFeatured = true,
            IsOnSale = false,
            MinRating = 4.0,
            SortBy = "price",
            SortDescending = false,
            Page = 2,
            PageSize = 10,
            Tags = ["wireless", "bluetooth"]
        };

        request.SearchQuery.Should().Be("headphones");
        request.CategoryId.Should().NotBeNull();
        request.BrandId.Should().NotBeNull();
        request.MinPrice.Should().Be(20m);
        request.MaxPrice.Should().Be(100m);
        request.InStockOnly.Should().BeTrue();
        request.IsFeatured.Should().BeTrue();
        request.IsOnSale.Should().BeFalse();
        request.MinRating.Should().Be(4.0);
        request.SortBy.Should().Be("price");
        request.SortDescending.Should().BeFalse();
        request.Page.Should().Be(2);
        request.PageSize.Should().Be(10);
        request.Tags.Should().HaveCount(2);
    }
}

public class ProductImageDtoTests
{
    [Fact]
    public void ProductImageDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductImageDto
        {
            Id = Guid.NewGuid(),
            Url = "https://example.com/image.jpg",
            AltText = "Product image",
            DisplayOrder = 1,
            IsPrimary = true
        };

        dto.Id.Should().NotBeEmpty();
        dto.Url.Should().Be("https://example.com/image.jpg");
        dto.AltText.Should().Be("Product image");
        dto.DisplayOrder.Should().Be(1);
        dto.IsPrimary.Should().BeTrue();
    }
}

public class ProductVariantDtoTests
{
    [Fact]
    public void ProductVariantDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductVariantDto
        {
            Id = Guid.NewGuid(),
            Name = "Red - Large",
            Sku = "SKU-001-RL",
            Price = 49.99m,
            CompareAtPrice = 59.99m,
            StockQuantity = 10,
            IsActive = true,
            Attributes = new Dictionary<string, string> { ["Color"] = "Red", ["Size"] = "Large" },
            ImageUrl = "https://example.com/variant.jpg"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Red - Large");
        dto.Sku.Should().Be("SKU-001-RL");
        dto.Price.Should().Be(49.99m);
        dto.StockQuantity.Should().Be(10);
        dto.IsActive.Should().BeTrue();
        dto.Attributes.Should().HaveCount(2);
    }
}

public class ProductStockDtoComprehensiveTests
{
    [Fact]
    public void ProductStockDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ProductStockDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            TotalStockQuantity = 100,
            ReservedQuantity = 20,
            LowStockThreshold = 10,
            WarehouseStocks =
            [
                new() { WarehouseId = Guid.NewGuid(), WarehouseName = "Main", WarehouseCode = "WH-001", Quantity = 60, ReservedQuantity = 10 },
                new() { WarehouseId = Guid.NewGuid(), WarehouseName = "Secondary", WarehouseCode = "WH-002", Quantity = 40, ReservedQuantity = 10 }
            ]
        };

        dto.ProductId.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.TotalStockQuantity.Should().Be(100);
        dto.ReservedQuantity.Should().Be(20);
        dto.AvailableQuantity.Should().Be(80);
        dto.LowStockThreshold.Should().Be(10);
        dto.IsLowStock.Should().BeFalse();
        dto.IsOutOfStock.Should().BeFalse();
        dto.WarehouseStocks.Should().HaveCount(2);
    }

    [Fact]
    public void ProductStockDto_IsLowStock_ShouldReturnTrueWhenBelowThreshold()
    {
        var dto = new ProductStockDto
        {
            TotalStockQuantity = 5,
            ReservedQuantity = 0,
            LowStockThreshold = 10
        };

        dto.IsLowStock.Should().BeTrue();
    }

    [Fact]
    public void ProductStockDto_IsOutOfStock_ShouldReturnTrueWhenZero()
    {
        var dto = new ProductStockDto
        {
            TotalStockQuantity = 0,
            ReservedQuantity = 0
        };

        dto.IsOutOfStock.Should().BeTrue();
    }
}

public class CreateProductRequestComprehensiveTests
{
    [Fact]
    public void CreateProductRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateProductRequest
        {
            Name = "Test Product",
            Description = "Description",
            ShortDescription = "Short",
            Price = 49.99m,
            CompareAtPrice = 69.99m,
            CostPrice = 30m,
            Sku = "SKU-001",
            Barcode = "1234567890123",
            StockQuantity = 100,
            LowStockThreshold = 10,
            IsActive = true,
            IsFeatured = false,
            IsDigital = false,
            RequiresShipping = true,
            Weight = 2.5,
            Length = 10.0,
            Width = 5.0,
            Height = 3.0,
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            MainImageUrl = "https://example.com/image.jpg",
            Images =
            [
                new() { Url = "https://example.com/image1.jpg", AltText = "Image 1", DisplayOrder = 1 },
                new() { Url = "https://example.com/image2.jpg", AltText = "Image 2", DisplayOrder = 2 }
            ],
            Variants =
            [
                new() { Name = "Red", Sku = "SKU-001-R", Price = 49.99m, StockQuantity = 50 },
                new() { Name = "Blue", Sku = "SKU-001-B", Price = 49.99m, StockQuantity = 50 }
            ],
            Tags = ["wireless", "bluetooth", "headphones"]
        };

        request.Name.Should().Be("Test Product");
        request.Price.Should().Be(49.99m);
        request.Sku.Should().Be("SKU-001");
        request.Images.Should().HaveCount(2);
        request.Variants.Should().HaveCount(2);
        request.Tags.Should().HaveCount(3);
    }
}

public class UpdateProductRequestComprehensiveTests
{
    [Fact]
    public void UpdateProductRequest_AllProperties_ShouldBeOptional()
    {
        var request = new UpdateProductRequest();

        request.Name.Should().BeNull();
        request.Description.Should().BeNull();
        request.ShortDescription.Should().BeNull();
        request.Price.Should().BeNull();
        request.CompareAtPrice.Should().BeNull();
        request.CostPrice.Should().BeNull();
        request.Sku.Should().BeNull();
        request.Barcode.Should().BeNull();
        request.StockQuantity.Should().BeNull();
        request.LowStockThreshold.Should().BeNull();
        request.IsActive.Should().BeNull();
        request.IsFeatured.Should().BeNull();
        request.IsDigital.Should().BeNull();
        request.RequiresShipping.Should().BeNull();
        request.Weight.Should().BeNull();
        request.Length.Should().BeNull();
        request.Width.Should().BeNull();
        request.Height.Should().BeNull();
        request.CategoryId.Should().BeNull();
        request.BrandId.Should().BeNull();
        request.MainImageUrl.Should().BeNull();
    }

    [Fact]
    public void UpdateProductRequest_WithValues_ShouldSetValues()
    {
        var request = new UpdateProductRequest
        {
            Name = "Updated Product",
            Price = 59.99m,
            StockQuantity = 200,
            IsActive = true,
            Tags = ["new-tag"]
        };

        request.Name.Should().Be("Updated Product");
        request.Price.Should().Be(59.99m);
        request.StockQuantity.Should().Be(200);
        request.IsActive.Should().BeTrue();
        request.Tags.Should().HaveCount(1);
    }
}
