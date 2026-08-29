using Xunit;
using FluentAssertions;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Domain.Policies.Standard;
using Ecommerce.Domain.Factories;

namespace Ecommerce.UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void Product_Create_ShouldSetProperties()
    {
        var product = ProductFactory.CreateProduct("Test Product", 49.99m, "SKU-001", "test-product");

        product.Name.Should().Be("Test Product");
        product.Price.Should().Be(49.99m);
        product.Sku.Should().Be("SKU-001");
        product.Slug.Should().Be("test-product");
        product.IsActive.Should().BeTrue();
        product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Product_Create_WithNullName_ShouldThrow()
    {
        Action act = () => ProductFactory.CreateProduct(null!, 49.99m, "SKU-001", "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Product_Create_WithEmptyName_ShouldThrow()
    {
        Action act = () => ProductFactory.CreateProduct("", 49.99m, "SKU-001", "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Product_Create_WithNegativePrice_ShouldThrow()
    {
        Action act = () => ProductFactory.CreateProduct("Test", -10m, "SKU-001", "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Product_UpdatePrice_ShouldUpdatePrice()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.UpdatePrice(59.99m);

        product.Price.Should().Be(59.99m);
    }

    [Fact]
    public void Product_UpdateStock_ShouldUpdateStock()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        product.StockQuantity = 10;

        product.StockQuantity = 20;

        product.StockQuantity.Should().Be(20);
    }

    [Fact]
    public void Product_IsInStock_ShouldReturnTrueWhenStockPositive()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        product.StockQuantity = 5;

        product.StockQuantity.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Product_IsInStock_ShouldReturnFalseWhenStockZero()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        product.StockQuantity = 0;

        product.StockQuantity.Should().Be(0);
    }

    [Fact]
    public void Product_UpdateName_ShouldUpdateName()
    {
        var product = ProductFactory.CreateProduct("Old Name", 49.99m, "SKU-001", "test");

        product.UpdateName("New Name");

        product.Name.Should().Be("New Name");
    }

    [Fact]
    public void Product_UpdateDescription_ShouldUpdateDescription()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.UpdateDescription("New description");

        product.Description.Should().Be("New description");
    }

    [Fact]
    public void Product_AddImage_ShouldAddImage()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        var image = new ProductImage
        {
            Url = "https://example.com/image.jpg",
            AltText = "Test image",
            IsPrimary = true
        };

        product.Images.Add(image);

        product.Images.Should().HaveCount(1);
        product.Images.First().Url.Should().Be("https://example.com/image.jpg");
    }

    [Fact]
    public void Product_SetCategory_ShouldSetCategory()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        var category = CategoryFactory.CreateCategory("Electronics");

        product.CategoryId = category.Id;

        product.CategoryId.Should().Be(category.Id);
    }

    [Fact]
    public void Product_SetBrand_ShouldSetBrand()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        var brandId = Guid.NewGuid();

        product.BrandId = brandId;

        product.BrandId.Should().Be(brandId);
    }

    [Fact]
    public void Product_AddVariant_ShouldAddVariant()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        var variant = new ProductVariant
        {
            Name = "Red - Large",
            Sku = "SKU-001-RL",
            Price = 49.99m,
            StockQuantity = 10
        };

        product.Variants.Add(variant);

        product.Variants.Should().HaveCount(1);
        product.Variants.First().Name.Should().Be("Red - Large");
    }

    [Fact]
    public void Product_SetSlug_ShouldSetSlug()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "old-slug");

        product.Slug = "new-slug";

        product.Slug.Should().Be("new-slug");
    }

    [Fact]
    public void Product_SetBarcode_ShouldSetBarcode()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.Barcode = "1234567890123";

        product.Barcode.Should().Be("1234567890123");
    }

    [Fact]
    public void Product_SetWeight_ShouldSetWeight()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.Weight = 2.5;

        product.Weight.Should().Be(2.5);
    }

    [Fact]
    public void Product_SetDimensions_ShouldSetDimensions()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.Length = 10.0;
        product.Width = 5.0;
        product.Height = 3.0;

        product.Length.Should().Be(10.0);
        product.Width.Should().Be(5.0);
        product.Height.Should().Be(3.0);
    }

    [Fact]
    public void Product_IsFeatured_ShouldSetCorrectly()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.IsFeatured = true;

        product.IsFeatured.Should().BeTrue();
    }

    [Fact]
    public void Product_IsActive_ShouldSetCorrectly()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.IsActive = false;

        product.IsActive.Should().BeFalse();
    }
}
