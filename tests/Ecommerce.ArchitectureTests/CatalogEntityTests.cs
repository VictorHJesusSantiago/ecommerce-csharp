using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.ArchitectureTests;

public class CatalogEntityTests
{
    [Fact]
    public void Product_ShouldHaveDefaultValues()
    {
        var product = new Product();
        product.IsActive.Should().BeTrue();
        product.IsFeatured.Should().BeFalse();
        product.AllowBackorder.Should().BeFalse();
        product.MaxOrderQuantity.Should().Be(10);
        product.TaxRate.Should().Be(0);
        product.Images.Should().NotBeNull();
        product.Variants.Should().NotBeNull();
    }

    [Fact]
    public void Product_ShouldCalculateAverageRating()
    {
        var product = new Product
        {
            Reviews = new List<ProductReview>
            {
                new() { Rating = 5 },
                new() { Rating = 4 },
                new() { Rating = 3 }
            }
        };
        product.AverageRating.Should().Be(4);
    }

    [Fact]
    public void Product_ShouldReturnZeroRating_WhenNoReviews()
    {
        var product = new Product { Reviews = new List<ProductReview>() };
        product.AverageRating.Should().Be(0);
    }

    [Fact]
    public void Product_ShouldCalculateReviewCount()
    {
        var product = new Product
        {
            Reviews = new List<ProductReview>
            {
                new(), new(), new(), new()
            }
        };
        product.ReviewCount.Should().Be(4);
    }

    [Fact]
    public void Product_ShouldReturnFalseForIsInStock_WhenNoVariants()
    {
        var product = new Product
        {
            Variants = new List<ProductVariant>(),
            StockQuantity = 0
        };
        product.IsInStock.Should().BeFalse();
    }

    [Fact]
    public void Product_ShouldReturnTrueForIsInStock_WhenHasStock()
    {
        var product = new Product
        {
            StockQuantity = 10,
            Variants = new List<ProductVariant>()
        };
        product.IsInStock.Should().BeTrue();
    }

    [Fact]
    public void Category_ShouldSupportHierarchy()
    {
        var parent = new Category { Name = "Electronics", Slug = "electronics" };
        var child = new Category { Name = "Phones", Slug = "phones", ParentId = parent.Id, Parent = parent };
        child.Parent.Should().Be(parent);
    }

    [Fact]
    public void Brand_ShouldHaveDefaultValues()
    {
        var brand = new Brand();
        brand.IsActive.Should().BeTrue();
        brand.LogoUrl.Should().BeNull();
    }

    [Fact]
    public void ProductImage_ShouldHaveDefaults()
    {
        var image = new ProductImage();
        image.SortOrder.Should().Be(0);
        image.IsPrimary.Should().BeFalse();
    }

    [Fact]
    public void ProductVariant_ShouldHaveDefaults()
    {
        var variant = new ProductVariant();
        variant.IsActive.Should().BeTrue();
        variant.StockQuantity.Should().Be(0);
    }

    [Fact]
    public void ProductTag_ShouldLinkToProduct()
    {
        var tag = new ProductTag { TagName = "new-arrival" };
        tag.TagName.Should().Be("new-arrival");
    }

    [Fact]
    public void StockMovement_ShouldRecordDetails()
    {
        var movement = new StockMovement
        {
            ProductId = Guid.NewGuid(),
            Quantity = 50,
            MovementType = "In",
            Reference = "PO-001",
            Notes = "Restocked"
        };

        movement.Quantity.Should().Be(50);
        movement.MovementType.Should().Be("In");
    }
}
