using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Common;

namespace Ecommerce.ArchitectureTests;

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Slug = "test-product",
            Description = "A test product",
            Price = 29.99m,
            Sku = "TEST-001",
            Barcode = "1234567890",
            Weight = 1.5m,
            Length = 10m,
            Width = 5m,
            Height = 3m,
            IsFeatured = true,
            IsActive = true,
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            StockQuantity = 100,
            LowStockThreshold = 10,
            AllowBackorder = false,
            MaxOrderQuantity = 10,
            TaxRate = 0.08m,
            MetaTitle = "Test Product - Meta",
            MetaDescription = "Meta description",
            ThumbnailUrl = "/images/product.jpg",
            CategoryName = "Electronics",
            BrandName = "TestBrand",
            AverageRating = 4.5m,
            ReviewCount = 25,
            VariantCount = 3,
            ImageCount = 5,
            IsInStock = true,
            IsOnSale = false,
            DiscountPercentage = 0,
            Tags = new List<string> { "tag1", "tag2" },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Name.Should().Be("Test Product");
        dto.Price.Should().Be(29.99m);
        dto.StockQuantity.Should().Be(100);
        dto.Tags.Should().HaveCount(2);
    }

    [Fact]
    public void CreateProductDto_ShouldHaveRequiredProperties()
    {
        var dto = new CreateProductDto
        {
            Name = "New Product",
            Description = "Description",
            Price = 19.99m,
            Sku = "NEW-001",
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            StockQuantity = 50,
            Weight = 0.5m,
            TaxRate = 0.1m
        };

        dto.Name.Should().Be("New Product");
        dto.Price.Should().Be(19.99m);
        dto.CategoryId.Should().NotBeEmpty();
    }

    [Fact]
    public void UpdateProductDto_ShouldHaveRequiredProperties()
    {
        var dto = new UpdateProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Product",
            Price = 39.99m
        };

        dto.Name.Should().Be("Updated Product");
        dto.Price.Should().Be(39.99m);
    }

    [Fact]
    public void ProductImageDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductImageDto
        {
            Id = Guid.NewGuid(),
            Url = "/images/product-1.jpg",
            AltText = "Product Image 1",
            SortOrder = 0,
            IsPrimary = true
        };

        dto.Url.Should().Be("/images/product-1.jpg");
        dto.IsPrimary.Should().BeTrue();
    }

    [Fact]
    public void ProductVariantDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductVariantDto
        {
            Id = Guid.NewGuid(),
            Name = "Size: Large",
            Sku = "VAR-001",
            Price = 34.99m,
            StockQuantity = 25,
            IsActive = true,
            Attributes = new Dictionary<string, string> { { "Size", "Large" }, { "Color", "Red" } }
        };

        dto.Name.Should().Be("Size: Large");
        dto.Attributes.Should().HaveCount(2);
        dto.Price.Should().Be(34.99m);
    }

    [Fact]
    public void ProductListDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductListDto
        {
            Products = new List<ProductDto> { new() { Name = "P1" }, new() { Name = "P2" } },
            TotalCount = 2,
            CurrentPage = 1,
            PageSize = 20,
            TotalPages = 1
        };

        dto.Products.Should().HaveCount(2);
        dto.TotalCount.Should().Be(2);
        dto.TotalPages.Should().Be(1);
    }

    [Fact]
    public void ProductSearchDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductSearchDto
        {
            Query = "laptop",
            CategoryId = Guid.NewGuid(),
            MinPrice = 100,
            MaxPrice = 2000,
            InStockOnly = true,
            SortBy = "price",
            SortOrder = "asc",
            Page = 1,
            PageSize = 20
        };

        dto.Query.Should().Be("laptop");
        dto.InStockOnly.Should().BeTrue();
    }
}
